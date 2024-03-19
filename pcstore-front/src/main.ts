import './assets/main.css'
import Vue, {createApp}  from '@vue/compat'
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import App from './App.vue'
import ProductList from './components/ProductList.vue';
import router from './router'

Vue.use(BootstrapVue);
const app = createApp(App)

app.use(router)
app.mount('#app')

app.component('ProductList', ProductList);

