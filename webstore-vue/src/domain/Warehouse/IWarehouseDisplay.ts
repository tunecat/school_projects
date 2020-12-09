import { IWarehouseEdit } from './IWarehouseEdit';
import { IItemInWarehouse } from '../ItemInWarehouse/IItemInWarehouse';

export interface IWarehouseDisplay extends IWarehouseEdit {
    warehouseItems: Array<IItemInWarehouse> | null;
}
