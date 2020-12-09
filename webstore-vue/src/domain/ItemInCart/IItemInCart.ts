import { IItemInCartCreate } from './IItemInCartCreate';
import IItem from '../Item/IItem';

export interface IItemInCart extends IItemInCartCreate{
    id: string;
    item: IItem | null;
    totalPriceWithDiscount: number;
    itemPriceWithDiscount: number;
    anount: number;
}
