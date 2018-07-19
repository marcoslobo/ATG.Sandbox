export class Order {
    id: number;
    side: string;
    quantity: string;
    symbol: string;
    price: number;
    
    constructor(id: number, side:string, quantity: string, symbol: string, price:number) {
        this.id = id;
        this.side = side;
        this.quantity = quantity;
        this.symbol = symbol;
        this.price = price;        
    }

    
}