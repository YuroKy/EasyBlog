import { VueEditor } from "vue2-editor";

export default {
  components: {
    VueEditor
  },
  data() {
    return {
      tag: {
        name: null,
      }
    };
  },
  async mounted() {
    if (this.$route.params.id) {
      var { data: tag } = await this.axios.get(`tags/${this.$route.params.id}`);
      this.tag = tag;
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
      }
      else {
        this.create();
      }
    },
    create() {
      this.axios
        .post('tags', this.tag)
        .then(() => this.$router.push({ name: "admin-tags" }));
    },
    update() {
      this.axios
        .put(`tags/${this.$route.params.id}`, this.tag)
        .then(() => this.$router.push({ name: "admin-tags" }));
    }
  },
};
