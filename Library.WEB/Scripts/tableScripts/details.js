function autorsDetailInit(e) {
    debugger;
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".units").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "GetDetails/" + e.data.Id
            },
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 7

        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "Title", title: "Edition title ", width: "300px" }
        ]
    });
}

function bookDetailInit(e) {
    debugger;
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".units").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "GetData/" + e.data.Id
            },
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 7

        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "ReleaseDate", title: "Release Date ", width: "300px" },
            { field: "Genre", title: "Genre", width: "300px" }
        ]
    });
}

function brochureDetailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".units").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "GetData/" + e.data.Id
            },
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 7

        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "ReleaseDate", title: "Release Date ", width: "300px" },
            { field: "Type", title: "Type", width: "300px" }
        ]
    });
}

function magazineDetailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".units").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "GetData/" + e.data.Id
            },
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 7

        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "ReleaseDate", title: "Release Date ", width: "300px" },
            { field: "IssueNumber", title: "Issue Number ", width: "300px" },
            { field: "Style", title: "Style", width: "300px" }
        ]
    });
}

function newspaperDetailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".units").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "GetData/" + e.data.Id
            },
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 7

        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "ReleaseDate", title: "Release Date ", width: "300px" },
            { field: "IssueNumber", title: "Issue Number ", width: "300px" },
            { field: "Style", title: "Style", width: "300px" }
        ]
    });
}