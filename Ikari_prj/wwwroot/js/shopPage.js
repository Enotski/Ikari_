var initDataGrid = function (gridContainer, dataUrl, gridColumns) {
	function isNotEmpty(value) {
		return value !== undefined && value !== null && value !== '';
	}
	var customDataSource = new DevExpress.data.CustomStore({
		key: 'Id',
		loadMode: 'processes',
		requireTotalCount: true,
		load: function (loadOptions) {
			var deferred = $.Deferred();
			var options = {};

			[
				"filter",
				"searchExpr",
				"searchOperation",
				"searchValue",
				"select",
				"sort",
				"skip",
				"take",
			].forEach(function (i) {
				if (i in loadOptions && isNotEmpty(loadOptions[i])) {
					options[i] = JSON.stringify(loadOptions[i]);
				}
			});

			$.ajax({
				url: dataUrl,
				type: 'POST',
				dataType: 'json',
				data: options,
				success(result) {
					deferred.resolve(result.data, {
						totalCount: result.totalCount,
					});
				},
				error() {
					deferred.reject('Data Loading Error');
				},
			});

			return deferred.promise();
		}
	});
	$(gridContainer).dxDataGrid({
		dataSource: customDataSource,
		showBorders: true,
		keyExpr: 'Id',
		remoteOperations: true,
		paging: {
			pageSize: 5,
		},
		pager: {
			showPageSizeSelector: true,
			allowedPageSizes: [5, 10, 20],
		},
		filterRow: {
			visible: true,
			applyFilter: 'auto',
		},
		sorting: {
			mode: 'multiple'
		},
		columns: gridColumns,
	}).dxDataGrid('instance');
};

const el = {
	data() {
		return {
			getSwordsPath: '/Shop/GetSwords',
			buyItemPath: '/Shop/BuyItem',
			showModal: false,
			selectedItem: null,
			weaponsTableBodyId: "#weaponsTable",
			armouryTableBodyId: "#armouryTable",
			weaponsColumns: [
				{
					dataField: 'Name',
					dataType: 'string',
					sortOrder: 1
				}, {
					dataField: 'Damage',
					dataType: 'number',
				},
			],
		}
	},
	created() {
	},
	mounted() {
		initDataGrid(this.weaponsTableBodyId, this.getSwordsPath, this.weaponsColumns);
	},
	methods: {
		onOpenModal: function (id, type) {
			this.showModal = !this.showModal;
			this.selectedItem = { id: id, type: type };
		},
		onAcceptModal: function () {
			var el = this;
			ajaxCall(this.buyItemPath, null, function (res) {
				var response = JSON.parse(res);
				alert(response.Text);
			});
			el.showModal = !el.showModal;
		},
		onCloseModal: function () {
			this.showModal = !this.showModal;
			this.selectedItem = null;
		}
	}
}

Vue.createApp(el).mount('#shopPage')