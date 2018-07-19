export class OrderResult {
    id: number;
    side: string;
    quantity: number;
    symbol: string;
    price: number;
    status: string;

    constructor(id: number, side:string, quantity: number, symbol: string, price:number, status:string) {
        this.id = id;
        this.side = side;
        this.quantity = quantity;
        this.symbol = symbol;
        this.price = price;
        this.status = status;
    }

    
}