<template>
    <div>
        <div v-if="!roles">NOT ALLOWED</div>
        <div v-else>
        <h1>{{commonTranslations.filter(t => t.code == "Create")[0].name}}</h1>
        <div style="color: red">{{errorMessage}}</div>
        <hr />
        <div v-for="error in errors" :key="error.id" class="text-danger">
            <div>{{ error.msg }}</div>
        </div>
        <div v-if="categoryCreateDTO" class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label" for="Name">{{categoriesViewTranslations.filter(t => t.code == "Name")[0].name}}</label>
                    <input
                        v-validate="'required|min:1|max:128'"
                        class="form-control"
                        type="text"
                        name="name"
                        data-val="true"
                        v-model="categoryCreateDTO.name"
                        data-val-required="The Name field is required."
                    />
                </div>
                <div>
                    <label class="typo__label">{{categoriesViewTranslations.filter(t => t.code == "ParentCategory")[0].name}}</label>
                    <multiselect
                        v-model="selectedCategory"
                        :options="parentCategories"
                        :searchable="false"
                        :show-labels="false"
                        label="name"
                        track-by="id"
                        placeholder="Pick a parent category"
                    ></multiselect>
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
import ICategoryCreate from '../../domain/Category/ICategoryCreate';
import ICategory from '../../domain/Category/ICategory';
import CultureDTO from '@/domain/Translations/CultureDTO'

@Component
export default class CategoriesCreateChild extends Vue {
    private selectedCategory: ICategory | null = null;
    errorMessage = "";

    get parentCategories(): ICategory[] | null {
        return store.state.categories;
    }

    get roles(): boolean {
        return store.getters.userRole.includes("Publisher");
    }

    get categoryCreateDTO(): ICategoryCreate {
        return {
            name: "",
            parentCategoryId: null
        };
    }

    onCreate(): void {
        if (this.categoryCreateDTO?.name!.length > 1) {
            if (!this.selectedCategory) {
                this.errorMessage = "Choose parent category";
            } else {
                this.categoryCreateDTO!.parentCategoryId = this.selectedCategory.id;
                store
                    .dispatch("createCategory", this.categoryCreateDTO)
                    .then(response => {
                        if (
                            response.statusCode >= 200 &&
                            response.statusCode < 300
                        ) {
                            router.push("/Profile");
                        } else {
                            this.errorMessage = JSON.parse(
                                response.errorMessage
                            ).message;
                        }
                    });
            }
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

    get categoriesViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "category") return t;
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
