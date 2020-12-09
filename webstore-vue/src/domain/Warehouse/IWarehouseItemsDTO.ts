import IItem from '../Item/IItem';

export interface IWarehouseItemsDTO {
    warehouse: string;
    items: Array<IItem> | null;
}
