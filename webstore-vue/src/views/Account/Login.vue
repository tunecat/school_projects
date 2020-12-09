<template>
    <div class="container">
        <main role="main" class="pb-3">
            <h1>Log in</h1>
            <div class="row">
                <div class="col-md-4">
                    <h4 style="color: red" v-if="loginResponse">{{loginResponse}}</h4>
                    <hr />
                    <div class="form-group">
                        <label for="Input_Email">Email</label>
                        <input
                            v-model="loginInfo.email"
                            class="form-control"
                            type="email"
                            id="Input_Email"
                            name="Input.Email"
                        />
                    </div>
                    <div class="form-group">
                        <label for="Input_Password">Password</label>
                        <input
                            v-model="loginInfo.password"
                            class="form-control"
                            type="password"
                            id="Input_Password"
                            name="Input.Password"
                        />
                    </div>
                    <div class="form-group">
                        <button @click="onLogin($event)" class="btn btn-primary">Log in</button>
                    </div>
                    <div class="form-group">
                        <p>
                            <a
                                id="forgot-password"
                                href="#/Account/ForgotPassword"
                            >Forgot your password?</a>
                        </p>
                        <p>
                            <a href="#/Account/Register">Register as a new user</a>
                        </p>
                    </div>
                </div>
            </div>
        </main>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { ILoginDTO } from "@/types/ILoginDTO";
import store from "@/store";
import router from "../../router";

@Component
export default class Login extends Vue {
    private loginResponse: string | null = null;

    private loginInfo: ILoginDTO = {
        email: "admin@mail.ru",
        password: "Qwertyui123!"
    };

    onLogin(event: Event): void {
        if (
            this.loginInfo.email.length > 0 &&
            this.loginInfo.password.length > 0
        ) {
            store
                .dispatch("authenticateUser", this.loginInfo)
                .then(response => {
                    if (response.statusCode === 200) {
                        this.loginResponse = null;
                        router.push("/");
                    } else {
                        if (response.statusCode === 403) {
                            this.loginResponse = "Wrong email or password";
                        } else {
                            this.loginResponse = response.errorMessage;
                        }
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
}
</script>
