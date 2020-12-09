<template>
    <div>
        <div v-if="!roles">NOT ALLOWED</div>
        <div v-else>
            <h1>{{commonTranslations.filter(t => t.code == "Create")[0].name}}</h1>
            <div>{{errorMessage}}</div>
            <h4>{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</h4>
            <hr />
            <div v-for="error in errors" :key="error.id" class="text-danger">
                <div>{{ error.msg }}</div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="Name">{{itemsViewTranslations.filter(t => t.code == "Name")[0].name}}</label>
                        <input
                            v-validate="'required|min:1|max:128'"
                            class="form-control"
                            type="text"
                            data-val="true"
                            name="Name"
                            v-model="brandsCreateDTO.name"
                            data-val-required="The Name field is required."
                            minlength="1"
                            maxlength="64"
                        />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Description">{{itemsViewTranslations.filter(t => t.code == "Description")[0].name}}</label>
                        <input
                            v-validate="'max:508'"
                            class="form-control"
                            type="text"
                            v-model="brandsCreateDTO.description"
                        />
                    </div>
                    <button
                        v-if="errors.all().length === 0"
                        type="submit"
                        @click="onCreate()"
                        class="btn btn-primary"
                    >{{commonTranslations.filter(t => t.code == "Create")[0].name}}</button>
                    <button v-else type="submit" class="btn btn-primary">{{commonTranslations.filter(t => t.code == "Create")[0].name}}</button>
                </div>
            </div>

            <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { IBrandsCreate } from "../../domain/Brands/IBrandsCreate";
import store from "@/store";
import router from "@/router";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class BrandsCreate extends Vue {
    private brandsCreateDTO: IBrandsCreate = {
        userId: null,
        name: "",
        description: null,
    };

    errorStatusMessage = "";
    errorMessage = "";

    get roles(): boolean {
        return store.getters.userRole.includes("Publisher");
    }

    onCreate(): void {
        if (this.brandsCreateDTO.description === "") {
            this.brandsCreateDTO.description = null;
        }
        console.log("name ", this.brandsCreateDTO.name);
        console.log("description ", this.brandsCreateDTO.description);
        if (this.brandsCreateDTO.name.length > 0) {
            store
                .dispatch("createBrand", this.brandsCreateDTO)
                .then((response) => {
                    if (
                        response.statusCode >= 200 &&
                        response.statusCode < 300
                    ) {
                        router.push("/Profile");
                    } else if (
                        response.statusCode >= 400 &&
                        response.statusCode < 500
                    ) {
                        this.errorStatusMessage = "NOT ALLOWED";
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
