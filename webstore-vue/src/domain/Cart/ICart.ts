import { ICartEdit } from './ICartEdit';
import { IItemInCart } from '../ItemInCart/IItemInCart';

export interface ICart extends ICartEdit {
    status: string;
    createdAt: string;
    payedAt: string | null;
    totalPrice: number;
    totalPriceWithoutVat: number;
    vat: number;
    cartItems: Array<IItemInCart> | null;
}
