import IItemEdit from './IItemEdit';
import { IBrands } from '../Brands/IBrands';
import ICategory from '../Category/ICategory';

export default interface IItem extends IItemEdit {
    itemPriceWithDiscount: number;
    categories: Array<ICategory> | null;
    brand: IBrands | null;
}
