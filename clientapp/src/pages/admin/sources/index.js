export default {
  data() {
    return {
      sources: [],
      fields: [
        {
          key: 'id',
          label: 'Ідентифікатор',
        },
        {
          key: 'name',
          label: 'Назва',
        },
        {
          key: 'Дії',
          label: 'Дії',
        }
      ],
    };
  },
  async mounted() {
    var { data: sources } = await this.axios.get('sources');
    this.sources = sources;
  },
  methods: {
    async remove(source) {
      await this.axios.delete(`sources/${source.id}`);
      this.sources = this.sources.filter(p => p.id != source.id);
      this.$toast.success("Джерело успішно видалено!");
    },
    edit(source) {
      this.$router.push({ name: 'source-editor', params: { id: source.id } });
    },
    create() {
      this.$router.push({ name: 'source-editor' });
    }
  },
};