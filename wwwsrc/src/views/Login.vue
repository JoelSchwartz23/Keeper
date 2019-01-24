<template>
    <div class="login">
        <form v-if="loginForm" @submit.prevent="loginUser">
            <input type="email" v-model="creds.email" placeholder="email">
            <input type="password" v-model="creds.password" placeholder="password">
            <button type="submit">Login</button>
        </form>
        <form v-else @submit.prevent="register">
            <input type="text" v-model="newUser.username" placeholder="name">
            <input type="email" v-model="newUser.email" placeholder="email">
            <input type="password" v-model="newUser.password" placeholder="password">
            <button type="submit">Create Account</button>
        </form>
        <div @click="loginForm = !loginForm">
            <p v-if="loginForm">No account Click to Register</p>
            <p v-else>Already have an account click to Login</p>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 keeps">
                    <div class="card" v-for="keep in getPublicKeeps">
                        <h4>{{keep.name}}</h4>
                        <img class="card-img" :src="keep.img" alt="card img">
                        <i class="fas fa-eye"> Views: {{keep.views}}</i>

                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "login",
        mounted() {
            //checks for valid session
            this.$store.dispatch("authenticate");
            this.$store.dispatch("getPublicKeeps");
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                }
            };
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        },
        computed: {
            getPublicKeeps() {
                return this.$store.state.publickeeps
            }
        }
    };
</script>