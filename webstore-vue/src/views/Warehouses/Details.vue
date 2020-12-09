<template>
    <div>
        <div v-if="statusErrorMessage">{{statusErrorMessage}}</div>
        <div v-else>
            <br />

            <div>
                <h4>{{warehousesTranslations.filter(t => t.code == "WarehouseUnit")[0].name}}</h4>
                <dl class="row">
                    <dt class="col-sm-2">{{warehousesTranslations.filter(t => t.code == "Name")[0].name}}</dt>
                    <dd class="col-sm-10">{{warehouseDTO.warehouse.name}}</dd>
                </dl>

                <hr />
                <h5>{{warehousesTranslations.filter(t => t.code == "WarehouseItems")[0].name}}</h5>
                <br />
                <table class="table">
                    <thead>
                        <tr>
                            <th>{{itemsViewTranslations.filter(t => t.code == "ItemUnit")[0].name}}</th>
                            <th>{{warehousesTranslations.filter(t => t.code == "ItemsInStock")[0].name}}</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr
                            v-for="itemInWarehouse in warehouseDTO.warehouse.warehouseItems"
                            :key="itemInWarehouse.id"
                        >
                            <td>
                                <a
                                    asp-controller="Items"
                                    asp-action="Details"
                                    asp-route-id="@warehouseItem.ItemId"
                                >{{itemInWarehouse.item.name}}</a>
                            </td>
                            <td>{{itemInWarehouse.itemsInStock}}</td>
                            <td>
                                <router-link
                                    :to="`/itemsInWarehouse/details/${itemInWarehouse.id}`"
                                >{{commonTranslations.filter(t => t.code == "Details")[0].name}}</router-link>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <a
                    class="btn btn-primary"
                    v-if="!showItemsActive && warehouseDTO.items.length >= 1"
                    @click="showItems()"
                >{{warehousesTranslations.filter(t => t.code == "AddItems")[0].name}}</a>
                <div id="addItems" v-if="showItemsActive">
                    <div class="form-group">
                        <multiselect
                            v-model="selectedItems"
                            :multiple="true"
                            :close-on-select="false"
                            :clear-on-select="false"
                            :preserve-search="true"
                            placeholder="Choose product"
                            label="name"
                            track-by="name"
                            :options="warehouseDTO.items"
                        >
                            <template slot="selection" slot-scope="{ values, search, isOpen }">
                                <span
                                    class="multiselect__single"
                                    v-if="values.length &amp;&amp; !isOpen"
                                >{{ values.length }} products selected</span>
                            </template>
                        </multiselect>
                        <div v-for="item in selectedItems" :key="item.id">{{item.name}}</div>
                    </div>
                    <div>
                        <input
                            @click="addItems(selectedItems)"
                            value="Add to warehouse"
                            class="btn btn-primary"
                        />
                        <a class="btn btn-secondary" @click="showItems()">{{commonTranslations.filter(t => t.code == "Cancel")[0].name}}</a>
                    </div>
                </div>
            </div>

            <router-link
                :to="`/warehouses/Edit/${id}`"
            >{{warehousesTranslations.filter(t => t.code == "EditWarehouseInfo")[0].name}}</router-link>|
            <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IBrands } from "../../domain/Brands/IBrands";
import store from "@/store";
import router from "@/router";
import { IWarehouseDisplay } from "../../domain/Warehouse/IWarehouseDisplay";
import { IWarehouseDTO } from "@/domain/Warehouse/IWarehouseDTO";
import { IWarehouseItemsDTO } from "@/domain/Warehouse/IWarehouseItemsDTO";
import IItem from "../../domain/Item/IItem";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class WarehouseDetails extends Vue {
    @Prop()
    private id!: string;

    statusErrorMessage = "";

    private selectedItems: Array<IItem> = [];

    private showItemsActive = false;

    get warehouseDTO(): IWarehouseDTO | null {
        return store.state.warehouse;
    }

    showItems(): void {
        this.showItemsActive = !this.showItemsActive;
    }

    addItems(items: Array<IItem>): void {
        console.log(items);
        if (items.length !== 0) {
            const warehouseItemsDTO: IWarehouseItemsDTO = {
                warehouse: this.warehouseDTO!.warehouse.id,
                items: items,
            };
            store
                .dispatch("AddItemsToWarehouse", warehouseItemsDTO)
                .then((response) => {
                    if (
                        response.statusCode >= 200 &&
                        response.statusCode < 300
                    ) {
                        store.dispatch("getWarehouse", this.id);
                        this.showItemsActive = !this.showItemsActive;
                    }
                });
        }
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        const response = store.dispatch("getWarehouse", this.id);
        response
            .then((response) => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.statusErrorMessage = "NOT ALLOWED";
                } else {
                    this.statusErrorMessage = JSON.parse(
                        response.statusErrorMessage
                    ).message;
                }
            })
            .catch((e) => {
                console.log(e);
            });
        console.log("mounted");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }

    get warehousesTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "warehouse") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

    get itemsViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "item") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

    get sharedTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const sharedTranslation = translations!.filter((t) => {
            if (t.entityName === "shared") return t;
        })[0].cultureDTO;
        return sharedTranslation;
    }

    get commonTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const commonTranslation = translations!.filter((t) => {
            if (t.entityName === "common") return t;
        })[0].cultureDTO;
        return commonTranslation;
    }
}
</script>
