export default {
  data() {
    return {
      entityName: '',
      options: [
        {
          text: 'Виберіть таблицю',
          value: '',
          disabled: true,
        },
        {
          text: 'Новини',
          value: 'Post',
        },
        {
          text: 'Теги',
          value: 'Tag',
        },
        {
          text: 'Користувачі',
          value: 'User',
        },
        {
          text: 'Джерела',
          value: 'Source',
        },
      ],
    }
  },
  methods: {
    async exportAsync() {
      if (!this.entityName) {
        return;
      }

      var { data: csv } = await this.axios.get(`export/${this.entityName}`);
      this.downloadForClient(csv, `${this.entityName}.csv`);
    },
    downloadForClient(fileData, fileName) {
      const url = window.URL.createObjectURL(new Blob([fileData]));
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', fileName);
      document.body.appendChild(link);
      link.click();
    },
  },
};