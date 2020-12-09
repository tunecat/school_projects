<template>
    <div>
        <div v-if="statusErrorMessage">{{statusErrorMessage}}</div>
        <div v-else>
            <p>
                <router-link
                    :to="`/warehouses/Create`"
                >{{warehousesTranslations.filter(t => t.code == "AddWarehouseToList")[0].name}}</router-link>
            </p>
            <table class="table">
                <thead>
                    <tr>
                        <th>{{warehousesTranslations.filter(t => t.code == "Name")[0].name}}</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="warehouse in warehouses" :key="warehouse.id">
                        <td>{{warehouse.name}}</td>
                        <td>
                            <router-link
                                :to="`/warehouses/Details/${warehouse.id}`"
                            >{{commonTranslations.filter(t => t.code == "Details")[0].name}}</router-link>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import { IWarehouse } from "../../domain/Warehouse/IWarehouse";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class WarehousesIndex extends Vue {
    statusErrorMessage = "";

    get warehouses(): IWarehouse[] | null {
        return store.state.warehouses;
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
        const response = store.dispatch("getWarehouses");
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
