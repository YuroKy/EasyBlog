export default {
  data() {
    return {
      posts: []
    };
  },
  async mounted() {
  },
  methods: {
    isActive(route) {
      return this.$route.path == route;
    }
  },
};