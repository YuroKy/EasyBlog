export default {
  data() {
    return {
      tags: [],
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
    var { data: tags } = await this.axios.get('tags');
    this.tags = tags;
  },
  methods: {
    async remove(tag) {
      await this.axios.delete(`tags/${tag.id}`);
      this.tags = this.tags.filter(p => p.id != tag.id);
    },
    edit(tag) {
      this.$router.push({ name: 'tag-editor', params: { id: tag.id } });
    },
    create() {
      this.$router.push({ name: 'tag-editor' });
    }
  },
};