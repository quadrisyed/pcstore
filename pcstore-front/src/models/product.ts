


export interface GraphicsCard {
    graphicsCardId: number;
    model: string | null;
    memory: string | null;
    brand: string | null;
   
}



export interface Processor {
    processorId: number;
    model: string | null;
    cores: number | null;
    brand: string | null;
   
}



export interface Psu {
    psuId: number;
    watts: number | null;
    brand: string | null;
   
}

export interface Storage {
    storageId: number;
    type: string | null;
    capacity: string | null;
    brand: string | null;
   
}
export interface Product {
    image : string,
    computerId: number;
    processorId: number | null;
    graphicsCardId: number | null;
    psuId: number | null;
    storageId: number | null;
    ramCapacity: number | null;
    ramUnit: string | null;
    weight: number | null;
    portConfig: string | null;
    graphicsCard: GraphicsCard | null;
    processor: Processor | null;
    psu: Psu | null;
    storage: Storage | null;    
    description: string ;
}