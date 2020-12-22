<template>
  <div id="app">
    <div>
      <b-navbar class="nav-shadow" toggleable="lg" type="light" variant="light">
        <b-navbar-brand to="/">EasyBlog - Легше не придумаєш </b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav> </b-collapse>
        <b-navbar-nav
          v-show="isAuthorized"
          class="float-right pull-right cursor-pointer"
        >
          <router-link to="/admin">Панель Адміністратора</router-link>
        </b-navbar-nav>
      </b-navbar>
    </div>
    <div class="wrapper container-fluid bg-color">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {};
  },
  computed: {
    isAuthorized() {
      if (!this.$store.getters) {
        return false;
      }

      return !!this.$store.getters.token;
    },
  },
  async mounted() {},
  methods: {},
};
</script>

<style>
#app {
  background: fixed linear-gradient(125deg, #b9c0ca 50%, #e9f0f9 100%), #e3edf7;
  height: 100%;
}

.bg-color {
  background: fixed linear-gradient(125deg, #b9c0ca 50%, #e9f0f9 100%), #e3edf7;
}

.cursor-pointer {
  cursor: pointer;
}

.nav-shadow {
  box-shadow: 0 4px 20px -2px rgba(135, 135, 135, 0.65);
}

pre {
  display: block;
  margin: 0 0 10px;
  white-space: pre-wrap;
  word-break: break-all;
  word-wrap: break-word;
  line-height: 1.45;
  overflow: auto !important;
  padding: 16px;
  background: #f1f8e9;
  border-radius: 4px;
  -webkit-box-shadow: 0 0 10px #f1f8e9;
  box-shadow: 0 0 10px #f1f8e9;
  border: 1px solid darkgray;
}
</style>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
