import Moment from "moment";

export default {
  data() {
    return {
      term: null,
      tags: [],
      filterTags: [],
      filterSources: [],
      posts: [],
      sources: [],
    };
  },
  async mounted() {
    await this.loadPosts();
    await this.loadTags();
    await this.loadSources();
  },
  methods: {
    async loadPosts() {
      var { data: posts } = await this.axios.get('posts');
      posts.forEach(p => p.createdTime = Moment(p.createdTime).format("DD/MM/YYYY HH:mm"));
      this.posts = posts;
    },
    async loadTags() {
      var { data: tags } = await this.axios.get('tags');
      this.tags = tags;
    },
    async loadSources() {
      var { data: sources } = await this.axios.get('sources');
      this.sources = sources;
    },
    async applyFilter() {
      await this.loadPosts();
      console.log("filtering");

      if (this.term) {
        this.posts = this.posts.filter(p => p.content.toLowerCase().includes(this.term.toLowerCase()) || p.title.toLowerCase().includes(this.term.toLowerCase()));
      }

      if (this.filterTags.length) {
        var filteredIds = this.filterTags.map(ft => ft.id);
        this.posts = this.posts.filter(p => p.tags.map(t => t.id).some(id => filteredIds.includes(id)));
      }

      if (this.filterSources.length) {
        var sourcefilteredIds = this.filterSources.map(ft => ft.id);
        this.posts = this.posts.filter(p => sourcefilteredIds.includes(p.source.id));
      }

      if (!this.term && !this.filterTags.length && !this.filterSources.length) {
        await this.resetFilter();
      }
    },
    async resetFilter() {
      this.term = null;
      this.filterTags = [];
      this.filterSources = [];
      
      await this.loadPosts();
    },
    getAuthorAvatar(post) {
      return `data:image/png;base64, ${post.authorAvatar}`;
    },
    getLink(post) {
      return { name: 'post-details', params: { id: post.id } };
    }
  },
};
