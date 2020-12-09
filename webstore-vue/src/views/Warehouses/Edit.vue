<template>
    <div>
        <div v-if="errorStatusMessage">{{errorStatusMessage}}</div>
        <div v-else>
            <h1>{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</h1>
            <div>{{errorMessage}}</div>
            <div v-for="error in errors" :key="error.id" class="text-danger">
                <div>{{ error.msg }}</div>
            </div>
            <div v-if="warehouseEditDTO" class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label
                            class="control-label"
                            for="Name"
                        >{{warehousesTranslations.filter(t => t.code == "Name")[0].name}}</label>
                        <input
                            v-validate="'required|min:1|max:128'"
                            class="form-control"
                            type="text"
                            name="name"
                            data-val="true"
                            v-model="warehouseEditDTO.name"
                            data-val-required="The Name field is required."
                            minlength="1"
                            maxlength="64"
                        />
                    </div>
                    <button
                        v-if="errors.all().length === 0"
                        type="submit"
                        @click="onEdit()"
                        class="btn btn-primary"
                    >{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</button>
                    <button
                        v-else
                        type="submit"
                        class="btn btn-secondary"
                    >{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</button>
                    <a
                        class="btn btn-danger"
                        @click="onDelete()"
                    >{{commonTranslations.filter(t => t.code == "Delete")[0].name}}</a>
                </div>
            </div>
            <div>
                <router-link
                    :to="`/Profile`"
                >{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>|
                <router-link :to="`/warehouses/Details/${this.id}`">Back to Warehouse</router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import router from "@/router";
import { IWarehouseEdit } from "../../domain/Warehouse/IWarehouseEdit";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class WarehouseEdit extends Vue {
    @Prop()
    private id!: string;

    get warehouseEditDTO(): IWarehouseEdit | null {
        const warehouse = store.state.warehouse;
        if (warehouse) {
            return {
                name: warehouse.warehouse.name,
                id: warehouse.warehouse.id,
                userId: warehouse.warehouse.userId,
            };
        }
        return null;
    }

    errorMessage = "";
    errorStatusMessage = "";

    onEdit(): void {
        if (this.warehouseEditDTO?.name!.length > 1) {
            store
                .dispatch("editWarehouse", this.warehouseEditDTO)
                .then((response) => {
                    if (
                        response.statusCode >= 200 &&
                        response.statusCode < 300
                    ) {
                        router.push("/Warehouses/Details/" + this.id);
                    } else {
                        this.errorMessage = JSON.parse(
                            response.errorMessage
                        ).message;
                    }
                });
        }
    }

    onDelete(): void {
        this.$confirm("Are you sure you want to delete this")
            .then(() => {
                store
                    .dispatch("deleteWarehouse", this.id)
                    .then((wasDeleted: boolean) => {
                        if (wasDeleted) {
                            router.push("/Warehouses");
                        }
                    });
            })
            .catch((e) => console.log(e));
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
        store
            .dispatch("getWarehouse", this.id)
            .then((response) => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.errorStatusMessage = "NOT ALLOWED";
                } else {
                    this.errorStatusMessage = JSON.parse(
                        response.errorStatusMessage
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

    get commonTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const commonTranslation = translations!.filter((t) => {
            if (t.entityName === "common") return t;
        })[0].cultureDTO;
        return commonTranslation;
    }
}
</script>
