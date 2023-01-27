var ajaxCall = function (path, data, successCall, errorCall) {
	$.ajax({
		url: path,
		type: 'POST',
		dataType: 'json',
		data: data,
		success: function (response) {
			if (successCall)
				successCall(response);
		},
		error: function (response) {
			if (errorCall)
				errorCall(response);
		}
	});
}
import  Notifications  from "@kyvg/vue3-notification";
const BaseApp = {
	data() {
		return {
			counter: 0,
			getUserInfoAction: '/UserProfile/GetUserInfo',
			homeVisible: false,
			shopVisible: false,
			communityVisible: false,
			profileVisible: false,
			historyVisible: false,
			currentUser: null,
			showRegistration: false,
			selectedItem: null,
		}
	},
	created() {

	},
	mounted() {
		this.getUserInfo();

		this.$notify("Hello user!");
	},
	methods: {
		getUserInfo: function () {
			var el = this;
			ajaxCall(this.getUserInfoAction, null, function (res) {
				if (res === "null" && !window.location.href.includes('UserProfile/Login')) {
					window.location.href = 'UserProfile/Login';
				}
				el.currentUser = JSON.parse(res);
				if (el.currentUser !== null && el.currentUser !== undefined) {
					el.setNavBtns();
				}
			});
		},
		setNavBtns: function () {
			this.homeVisible = true;
			this.shopVisible = true;
			this.communityVisible = true;
			this.profileVisible = true;
			this.historyVisible = this.currentUser.Role.Name === 'Admin' || this.currentUser.Role.Name === 'Moderator';
		},
		changeForm: function () {
			this.showRegistration = !this.showRegistration;
		},
	}
}

Vue.createApp(BaseApp).use(Notifications).mount('#baseApp')