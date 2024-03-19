<template>
    <b-modal v-model="createModalOpen" @ok="createProduct()" title="Create Computer">
        <form @submit.prevent="submitForm">
        <b-form-group label="Processor">
            <b-form-select v-model="currentProduct.processorId" :options="processorOptions"></b-form-select>
        </b-form-group>
        
        <b-form-group label="Graphics Card">
            <b-form-select v-model="currentProduct.graphicsCardId" :options="gpuOptions"></b-form-select>
        </b-form-group>

        <b-form-group label="PSU">
            <b-form-select v-model="currentProduct.psuId" :options="psuOptions"></b-form-select>
        </b-form-group>

        <b-form-group label="Storage">
            <b-form-select v-model="currentProduct.storageId" :options="storageOptions"></b-form-select>
        </b-form-group>
        <b-form-group label="Memory">
            <b-form-select v-model="currentProduct.memoryId" :options="memoryOptions"></b-form-select>
        </b-form-group>
        
        </form>
    </b-modal>    
    <b-modal v-model="editModalOpen" @ok="saveProduct()" title="Edit Computer">
        <form @submit.prevent="submitForm">
        <b-form-group label="Processor">
            <b-form-select v-model="currentProduct.processorId" :options="processorOptions"></b-form-select>
        </b-form-group>
        
        <b-form-group label="Graphics Card">
            <b-form-select v-model="currentProduct.graphicsCardId" :options="gpuOptions"></b-form-select>
        </b-form-group>

        <b-form-group label="PSU">
            <b-form-select v-model="currentProduct.psuId" :options="psuOptions"></b-form-select>
        </b-form-group>

        <b-form-group label="Storage">
            <b-form-select v-model="currentProduct.storageId" :options="storageOptions"></b-form-select>
        </b-form-group>
        <b-form-group label="Memory">
            <b-form-select v-model="currentProduct.memoryId" :options="memoryOptions"></b-form-select>
        </b-form-group>
 
        </form>
    </b-modal>
    <div class="d-flex justify-content-start column-container">
        <!-- Filter Column -->
        <div class="filter-column">
            <h2>Filters</h2>
            <!-- Your filter controls go here -->
            <div class="">
                

                <div>
                    <h3 for="processor">Processors:</h3>
                    <div id="processor" v-for="(item, index) in processors" :key="index">
                        <input type="checkbox" :id="'checkbox-' + index" :value="filter.processor[index]"
                            :checked="filter.processor[index] == item.processorId"
                            @click="handleClickDebounced('processor', item)">
                        <label class="pl-1" :for="'checkbox-' + index">{{ item.text }}</label>
                    </div>
                </div>
                <div>
                    <h3 for="gpu">Graphics Card:</h3>
                    <div id="gpu" v-for="(item, index) in gpu" :key="index">
                        <input type="checkbox" :id="'checkbox-' + index" :value="filter.gpu[index]"
                            :checked="filter.gpu[index] == item.graphicCardId" @click="handleClickDebounced('gpu', item)">
                        <label class="pl-1" :for="'checkbox-' + index">{{ item.text }}</label>
                    </div>
                </div>
                <div>
                    <h3 for="storage">Storage:</h3>
                    <div id="storage" v-for="(item, index) in storage" :key="index">
                        <input type="checkbox" :id="'checkbox-' + index" :value="item.storageId"
                            :checked="filter.storage[index] == item.storageId"
                            @click="handleClickDebounced('storage', item)">
                        <label class="pl-1" :for="'checkbox-' + index">{{ item.text }}</label>
                    </div>
                </div>
                <div>
                    <h3 for="psu">Memory:</h3>
                    <div id="psu" v-for="(item, index) in memory" :key="index">
                        <input type="checkbox" :id="'checkbox-' + index" :value="item.memoryId"
                            :checked="filter.memory[index] == item.memoryId" @click="handleClickDebounced('memory', item)">
                        <label class="pl-1" :for="'checkbox-' + index">{{ item.text }} </label>
                    </div>
                </div>                
                <div>
                    <h3 for="psu">Power Supply:</h3>
                    <div id="psu" v-for="(item, index) in psu" :key="index">
                        <input type="checkbox" :id="'checkbox-' + index" :value="item.psuId"
                            :checked="filter.psu[index] == item.psuId" @click="handleClickDebounced('psu', item)">
                        <label class="pl-1"  :for="'checkbox-' + index">{{ item.text }} </label>
                    </div>
                </div>

            </div>

            <!-- Listing Column -->
        </div>
        <div class="listing-column">
                <h2 class="d-inline">Listings</h2>
                    <div class="float-right">                            
                        <b-button style="btn" @click="showCreateModal()">Add</b-button>                      
                    </div>
                    
                    <div class="product-tile" v-for="(product, index) in filteredProducts" :key="index">
    
                        <div v-if="description(product.description) !== undefined">
                            <h3>{{ description(product.description).Processor }}</h3>
                            <p>Power:{{ description(product.description).PSU }}</p>
                            <p>GPU:{{ description(product.description).GPU }}</p>
                            <p>Storage:{{ description(product.description).Storage }}</p>
                        </div>
                        <label >Ports:</label>
                        <div v-for="(port,index) in  portConfig(product.portConfig)" :key="index">
                            <p>{{ port[0] }} : {{  port[1] }}</p>
                        </div>  
                        <div class="button-group">
                            <b-button style="btn" @click="showEditModal(product)">Edit</b-button>                      
                            <b-button @click="showConfirm(product)">Delete</b-button>
                        </div>
                    </div>
                <!-- Your listing content goes here -->
        </div>
        <!-- Product list -->
    </div>

</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from 'vue';
import ProductService from '@/services/productlist.service';

export default defineComponent({
    data() {
        return {
            editModalOpen:false,
            createModalOpen:false,
            currentProduct:{},
            searchQuery: '',
            selectedCategory: '',
            total:0,
            products: [], // Array of products
            processors: [],
            psu: [],
            gpu: [],
            memory:[],
            storage: [],
            filterdCount: 0,
            checkedItems: [],
            filter: {
                processor: [],
                gpu: [],
                storage: [],
                psu: [],
                memory:[]
            }
        };
    },
    computed: {
        processorOptions() {
            return this.processors.map(p=> {return {value:p.processorId, text:p.text};})
        },
        gpuOptions() {
            return this.gpu.map(p=> {return {value:p.graphicCardId, text:`${p.text}`};})
        },
        psuOptions() {
            return this.psu.map(p=> {return {value:p.psuId, text:`${p.text}`};})
        },    
        
        storageOptions() {
            return this.storage.map(p=> {return {value:p.storageId, text:`${p.text}`};})
        },  
        memoryOptions() {
            return this.memory.map(p=> {return {value:p.memoryId, text:`${p.text}`};})
        }, 
        filteredProducts() {
            return this.products.filter(p=> p.computerId !== undefined);
        },
    },
    methods: {
        async fetchProducts() {
            try {
                const productService = new ProductService();
                if (productService) {
                    let filters = this.getFilter();
                    var response = await productService.searchProducts<any>(filters);
                    this.products = response.computers.$values;
                    this.total = response.count;
                }
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        },
        async fetchFilters() {
            try {
                const productService = new ProductService();
                if (productService) {
                    var response = await productService.getFilters<any>();
                    this.psu = response.psu.$values;
                    this.processors = response.processor.$values;
                    this.storage = response.storage.$values;
                    this.gpu = response.gpus.$values;
                    this.memory = response.memory.$values;
                }
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        },
        searchProduct() {
            clearTimeout(this.debounceTimeoutSearch);
            this.debounceTimeoutSearch = setTimeout(() => {
                this.fetchProducts();
            });
        },
        async saveProduct(){
            let productService = new ProductService()            
            var response = await productService.saveProduct(this.currentProduct);
            //TODO::implement toast notification
        },
        async createProduct(){
            let productService = new ProductService()            
            var response = await productService.createProduct(this.currentProduct);
            //TODO::implement toast notification
        },
        showConfirm(product) {
            this.boxOne = ''
            this.$bvModal.msgBoxConfirm(`Are you sure, you want to delete?`)
            .then(value => {
                if(value) {
                    this.deleteProduct(product)
                }
            })
            .catch(err => {
                // An error occurred
            })
        },
        async deleteProduct(product) {
            //alert(product);
            //return;
            let productService = new ProductService()            
            var response = await productService.deleteProduct(product); 
        },
        description(desc) {
            if(desc)
                return  JSON.parse(desc);
            return undefined;
        },
        portConfig(portc) {
            if(portc)
                return  Object.entries(JSON.parse(portc));
            return undefined;
        },
        showCreateModal(){
            this.createModalOpen = true;
        },
        showEditModal(product) {
            this.currentProduct = product;
            this.editModalOpen = true;
        },
        handleClickDebounced(type, value) {
            clearTimeout(this.debounceTimeout);

            this.debounceTimeout = setTimeout(() => {
                if (type === 'processor') {
                    this.toggleSelectedValue('processor', value.processorId);
                } else if (type === 'storage') {
                    this.toggleSelectedValue('storage', value.storageId);
                } else if (type === 'psu') {
                    this.toggleSelectedValue('psu', value.psuId);
                } else if (type === 'gpu') {
                    this.toggleSelectedValue('gpu', value.graphicsCardId);
                }
            }, 300);
            clearTimeout(this.debounceTimeoutFetch);
            this.debounceTimeoutFetch = setTimeout(() => {
                this.fetchProducts();
            }, 300)

        },
        toggleSelectedValue(listName, value) {
            const index = this.filter[listName].indexOf(value);
            if (index === -1) {
                this.filter[listName].push(value);
            } else {
                this.filter[listName].splice(index, 1);
            }


        },
        getFilter() {
            return {
                ProcessorId: this.filter.processor,
                GraphicsCardId: this.filter.gpu,
                PsuId: this.filter.psu,
                StorageId: this.filter.storage
            };
      
        }

    },
    onMounted: () => {

    },
    created() {
        this.fetchProducts();
        this.fetchFilters();
    },
});
</script>

<style>
.product-tile {
    border: 1px solid #ccc;
    border-radius: 5px;
    padding: 10px;
    margin: 10px;
    width: calc(100% - 20px);
    display: inline-block;
    vertical-align: top;
}

.product-tile h3 {
    margin-top: 0;
}

.button-group {
    text-align: right;
}

button {
    margin: 5px;
}

.column-container {
      display: flex;
      
    }
    .filter-column {
      flex: 0 0 100%; /* Initially, take up the full width */
      max-width: 100%;
      background-color: #f0f0f0;
      padding: 20px;
      box-sizing: border-box;
    }
    .listing-column {
      flex: 0 0 100%; /* Initially, take up the full width */
      max-width: 100%;
      background-color: #e0e0e0;
      padding: 20px;
      box-sizing: border-box;
    }

    @media (min-width: 768px) {
      .filter-column {
        flex: 0 0 30%; /* Take up 20% of the width on larger screens */
        max-width: 30%;
      }
      .listing-column {
        flex: 0 0 70%; /* Take up 80% of the width on larger screens */
        max-width: 70%;
      }
    }
</style>