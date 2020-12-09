import { IWarehouse } from './IWarehouse';
import IItem from '../Item/IItem';
import { IWarehouseDisplay } from './IWarehouseDisplay';

export interface IWarehouseDTO {
    warehouse: IWarehouseDisplay;
    items: Array<IItem> | null;
}
