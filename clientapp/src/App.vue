<template>
  <div id="app">
    <div>
      <b-navbar toggleable="lg" type="dark" variant="dark">
        <b-navbar-brand to="/">EasyBlog - Легше не придумаєш </b-navbar-brand>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
        <b-collapse id="nav-collapse" is-nav> </b-collapse>
        <b-navbar-nav> </b-navbar-nav>
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
    return {
      test: "Yurii",
    };
  },
  async mounted() {},
  methods: {},
};
</script>

<style>
#app {
  background-color: #faf0e6;
  height: 100%;
}

.bg-color {
  background-color: #faf0e6;
}

.cursor-pointer {
  cursor: pointer;
}
</style>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
