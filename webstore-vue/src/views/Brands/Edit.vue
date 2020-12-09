<template>
    <div>
        <div v-if="errorStatusMessage">{{errorStatusMessage}}</div>
        <div v-else>
            <h1>{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</h1>
            <div>{{errorMessage}}</div>
            <h4>{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</h4>
            <hr />
            <div v-for="error in errors" :key="error.id" class="text-danger">
                <div>{{ error.msg }}</div>
            </div>
            <div v-if="brandEditDTO" class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="Name">{{brandsViewTranslations.filter(t => t.code == "Name")[0].name}}</label>
                        <input
                            v-validate="'required|min:1|max:128'"
                            class="form-control"
                            type="text"
                            name="name"
                            data-val="true"
                            v-model="brandEditDTO.name"
                            data-val-required="The Name field is required."
                            minlength="1"
                            maxlength="64"
                        />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Description">{{brandsViewTranslations.filter(t => t.code == "Description")[0].name}}</label>
                        <input
                            v-validate="'max:508'"
                            class="form-control"
                            type="text"
                            v-model="brandEditDTO.description"
                        />
                    </div>
                    <button
                        v-if="errors.all().length === 0"
                        type="submit"
                        @click="onEdit()"
                        class="btn btn-primary"
                    >{{sharedTranslations.filter(t => t.code == "Save")[0].name}}</button>
                    <button v-else type="submit" class="btn btn-primary">{{sharedTranslations.filter(t => t.code == "Save")[0].name}}</button>
                </div>
            </div>

            <div>
                <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IBrandsEdit } from "../../domain/Brands/IBrandsEdit";
import store from "@/store";
import router from "@/router";
import CultureDTO from '@/domain/Translations/CultureDTO';

@Component
export default class BrandsEdit extends Vue {
    @Prop()
    private id!: string;

    get brandEditDTO(): IBrandsEdit | null {
        const brand = store.state.brand;
        if (brand) {
            return {
                userId: null,
                name: brand.name,
                description: brand.description,
                id: brand.id
            };
        }
        return null;
    }

    errorMessage = "";
    errorStatusMessage = "";

    onEdit(): void {
        if (this.brandEditDTO?.description === "") {
            this.brandEditDTO.description = null;
        }
        console.log("name ", this.brandEditDTO?.name);
        console.log("description ", this.brandEditDTO?.description);
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        if (this.brandEditDTO!.name!.length > 1) {
            store.dispatch("editBrand", this.brandEditDTO).then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    router.push("/Profile");
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
        store
            .dispatch("getBrand", this.id)
            .then(response => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.errorStatusMessage = "NOT ALLOWED";
                } else {
                    this.errorStatusMessage = JSON.parse(
                        response.errorStatusMessage
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

    get brandsViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "brand") return t;
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
