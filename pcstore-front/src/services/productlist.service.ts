// DataService.ts
import axios   from 'axios';

const baseURL = 'http://localhost:5125/api'; // Replace with your API base URL

export default class ProductService {
    public searchProducts= async<T>(filter: any): Promise<T[]>=>  {
    try {
      const response: any = await axios.post<T[]>(`${baseURL}/Products/Search`,
      JSON.stringify(filter),
      {
        headers: { 'Content-Type': 'application/json' }
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error; // Throw error to handle it in the component
    }
    }
    public getFilters = async(): Promise<T[]> => {
    try {
      const response: any = await axios.get<T[]>(`${baseURL}/lov`);
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error; // Throw error to handle it in the component
    }
    }

    public saveProduct= async<T>(product: any): Promise<T>=>  {
        try {
          const response: any = await axios.put<T[]>(`${baseURL}/Products/${product.computerId}`,
          JSON.stringify(product),
          {
            headers: { 'Content-Type': 'application/json' }
          });
          return response.data;
        } catch (error) {
          console.error('Error saving data:', error);
          throw error; // Throw error to handle it in the component
        }
    }

    public createProduct= async<T>(product: any): Promise<T>=>  {
        try {
          const response: any = await axios.post<T[]>(`${baseURL}/Products`,
          JSON.stringify(product),
          {
            headers: { 'Content-Type': 'application/json' }
          });
          return response.data;
        } catch (error) {
          console.error('Error creating computer:', error);
          throw error; // Throw error to handle it in the component
        }
    }

    public deleteProduct= async<T>(product: any): Promise<T>=>  {
        try {
          const response: any = await axios.delete<T[]>(`${baseURL}/Products/${product.computerId}`);
          return response.data;
        } catch (error) {
          console.error('Error deleting data:', error);
          throw error; // Throw error to handle it in the component
        }
    }    
}
