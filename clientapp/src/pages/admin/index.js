export default {
  data() {
    return {
      posts: [],
    };
  },
  async mounted() {
    if (!this.isAuthorized) {
      this.$router.push({ name: "sign-in" });
    }
  },
  computed: {
    isAuthorized() {
      return !!this.$store.getters.token;
    },
  },
  methods: {
    isActive(route) {
      return this.$route.path == route;
    }
  },
};