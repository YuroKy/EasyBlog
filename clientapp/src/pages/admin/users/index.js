import Moment from "moment";

export default {
  data() {
    return {
      users: [],
      fields: [
        {
          key: 'id',
          label: 'Ідентифікатор',
        },
        {
          key: 'firstName',
          label: "Ім'я",
        },
        {
          key: 'lastName',
          label: "Фамілія",
        },
        {
          key: 'email',
          label: "Пошта",
        },
        {
          key: 'registrationTime',
          label: "Час реєстрації",
          formatter: (value) => Moment(value).format("DD/MM/YYYY HH:mm")
        },
        {
          key: 'status',
          label: "Стан",
          formatter: (status) => status === 0 ? 'Активний' : 'Заблокований'
        },

        {
          key: 'Дії',
          label: 'Дії',
        }
      ],
    };
  },
  async mounted() {
    await this.loadUsers();
  },
  methods: {
    async loadUsers() {
      var { data: users } = await this.axios.get('users');
      this.users = users;
    },
    async remove(user) {
      await this.axios.delete(`users/${user.id}`);
      this.users = this.users.filter(p => p.id != user.id);
    },
    edit(user) {
      this.$router.push({ name: 'user-editor', params: { id: user.id } });
    },
    create() {
      this.$router.push({ name: 'user-editor' });
    },
    changePassword(user) {
      this.$router.push({ name: 'user-password-changer', params: { id: user.id } });
    },
    async changeUserStatus(user) {
      const action = user.status === 0 ? 'block' : 'unblock';
      await this.axios.put(`users/${action}/${user.id}`);
      await this.loadUsers();
    }
  },
};