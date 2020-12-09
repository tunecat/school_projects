<template>
    <div>
        <div v-if="statusErrorMessage">{{statusErrorMessage}}</div>
        <div v-else>
            Warehouse - {{itemDTO.warehouse.name}}
            <hr />
            <p style="color: red">{{errorMessage}}</p>

            <dl class="row">
                <dt class="col-sm-2">Product</dt>

                <dd class="col-sm-10">{{itemDTO.item.name}}</dd>

                <dt class="col-sm-2">Items In Stock</dt>

                <dd class="col-sm-10">
                    {{itemDTO.itemsInStock}} ->
                    <input type="number" v-model="amount" min="0" />
                    <a @click="increaseAmount()">+</a>
                    <a @click="decreaseAmount()">-</a>
                    <a @click="nullifyAmount()">Empty</a>
                </dd>
            </dl>
        </div>
        <div>
            <a @click="onDelete()">RemoveItem</a>
            <router-link :to="`/warehouses/Details/${id}`">BackToWarehouse</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import { IItemInWarehouseDisplay } from "../../domain/ItemInWarehouse/IItemInWarehouseDisplay";
import router from "@/router";

@Component
export default class Details extends Vue {
    @Prop()
    private id!: string;

    amount = 0;

    errorMessage = "";
    statusErrorMessage = "";

    get itemDTO(): IItemInWarehouseDisplay | null {
        return store.state.itemInWarehouse;
    }

    increaseAmount() {
        this.changeAmount("plus");
    }

    decreaseAmount() {
        if (this.itemDTO!.itemsInStock < this.amount) {
            this.errorMessage = "Not enough products in stock";
        } else {
            this.changeAmount("minus");
        }
    }

    nullifyAmount() {
        if (this.itemDTO!.itemsInStock !== 0) this.changeAmount("Empty");
    }

    changeAmount(operation: string) {
        store
            .dispatch("ChangeAmount", {
                itemInWarehouseId: this.id,
                amount: this.amount,
                operation: operation
            })
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    store.dispatch("getItemInWarehouse", this.id);
                }
            })
            .catch(e => console.log(e));
    }

    onDelete(): void {
        this.$confirm("Are you sure you want to delete this")
            .then(() => {
                store
                    .dispatch("deleteItemInWarehouse", this.id)
                    .then((wasDeleted: boolean) => {
                        if (wasDeleted) {
                            router.push(
                                "/warehouses/Details/" +
                                    this.itemDTO!.warehouse!.id
                            );
                        }
                    });
            })
            .catch(e => console.log(e));
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
        const response = store.dispatch("getItemInWarehouse", this.id);
        response
            .then(response => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.statusErrorMessage = "NOT ALLOWED";
                } else {
                    this.statusErrorMessage = JSON.parse(
                        response.statusErrorMessage
                    ).message;
                }
            })
            .catch(e => {
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
}
</script>
