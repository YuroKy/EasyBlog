export default {
  data() {
    return {
      model: {
        username: null,
        password: null,
      }
    };
  },
  async mounted() {
  },
  methods: {
    async signIn() {
      this.axios.post('auth', this.model)
        .then((r) => {
          this.$store.commit('updateToken', r.data);
          this.$router.push({ name: 'admin-posts' });
        })
        .catch(() => this.$toast.error("Не вірний логін або пароль!"));
    }
  },
};