
export default {
  data() {
    return {
      source: {
        name: null,
      }
    };
  },
  async mounted() {
    if (this.$route.params.id) {
      var { data: source } = await this.axios.get(`sources/${this.$route.params.id}`);
      this.source = source;
    }
  },
  computed: {
    buttonName() {
      return this.$route.params.id ? "Оновити" : "Створити";
    },
  },
  methods: {
    createOrUpdate() {
      if (this.$route.params.id) {
        this.update();
        this.$toast.success("Джерело успішно оновлено!");
      }
      else {
        this.create();
        this.$toast.success("Джерело успішно створено!");
      }
    },
    create() {
      this.axios
        .post('sources', this.source)
        .then(() => this.$router.push({ name: "admin-sources" }));
    },
    update() {
      this.axios
        .put(`sources/${this.$route.params.id}`, this.source)
        .then(() => this.$router.push({ name: "admin-sources" }));
    }
  },
};
