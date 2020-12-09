export default interface IItemCreate {
    name: string;
    userId: string | null;
    brandId: string;
    description: string | null;
    price: number;
    discount: number;
    itemCategories: null | Array<string>; // for creation
}
