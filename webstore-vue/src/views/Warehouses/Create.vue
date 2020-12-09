<template>
    <div>
        <div v-if="!roles">NOT ALLOWED</div>
        <div v-else>
            <h1>{{commonTranslations.filter(t => t.code == "Create")[0].name}}</h1>
            <div>{{errorMessage}}</div>
            <div v-for="error in errors" :key="error.id" class="text-danger">
                <div>{{ error.msg }}</div>
            </div>
            <div v-if="warehouseCreateDTO" class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="Name">{{warehousesTranslations.filter(t => t.code == "Name")[0].name}}</label>
                        <input
                            v-validate="'required|min:2|max:128'"
                            class="form-control"
                            type="text"
                            name="name"
                            data-val="true"
                            v-model="warehouseCreateDTO.name"
                            data-val-required="The Name field is required."
                        />
                    </div>
                    <button
                        v-if="errors.all().length === 0"
                        type="submit"
                        @click="onCreate()"
                        class="btn btn-primary"
                    >{{commonTranslations.filter(t => t.code == "Create")[0].name}}</button>
                    <button v-else type="submit" class="btn btn-secondary">{{commonTranslations.filter(t => t.code == "Create")[0].name}}</button>
                </div>
            </div>
            <div>
                <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IBrandsCreate } from "../../domain/Brands/IBrandsCreate";
import store from "@/store";
import router from "@/router";
import ICategoryCreate from "../../domain/Category/ICategoryCreate";
import ICategory from "../../domain/Category/ICategory";
import { IWarehouseCreate } from "../../domain/Warehouse/IWarehouseCreate";
import CultureDTO from '@/domain/Translations/CultureDTO';

@Component
export default class WarehouseCreate extends Vue {
    errorMessage = "";

    get warehouseCreateDTO(): IWarehouseCreate {
        return {
            name: "",
            userId: null
        };
    }

    get roles(): boolean {
        return store.getters.userRole.includes("Publisher");
    }

    onCreate(): void {
        if (this.warehouseCreateDTO?.name!.length > 1) {
            store
                .dispatch("createWarehouse", this.warehouseCreateDTO)
                .then(response => {
                    if (
                        response.statusCode >= 200 &&
                        response.statusCode < 300
                    ) {
                        router.push("/warehouses");
                    } else {
                        this.errorMessage = JSON.parse(
                            response.errorMessage
                        ).message;
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

    get commonTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const commonTranslation = translations!.filter((t) => {
            if (t.entityName === "common") return t;
        })[0].cultureDTO;
        return commonTranslation;
    }
}
</script>
