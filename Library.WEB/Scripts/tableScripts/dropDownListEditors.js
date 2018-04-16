function autorsDropDownEditor(container, options) {
    $('<input data-text-field="AutorName" data-value-field="Id" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            optionLabel: "Select author",
            dataTextField: "AutorName",
            dataValueField: "Id",
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "/api/Autors/",
                        dataType: "json",
                        type: "GET"
                    }
                }
            }
        });
}

function bookGenreDropDownEditor(container, options) {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            optionLabel: "Select genre",
            dataSource: {
                type: "json",
                transport: {
                    read: "/api/Books/Genre"
                }
            }
        });
}

function brochureTypeDropDownEditor(container, options) {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            optionLabel: "Select Type",
            dataSource: {
                type: "json",
                transport: {
                    read: "/api/Brochures/GenreList"
            }
        }
    });
}

function magazineStyleDropDownEditor(container, options) {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            optionLabel: "Select genre",
            dataSource: {
                type: "json",
                transport: {
                    read: "/api/Magazines/GenreList"
            }
        }
    });
}

function newspaperTypeDropDownEditor(container, options) {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            optionLabel: "Select genre",
            dataSource: {
                type: "json",
                transport: {
                    read: "/api/Newspapers/GenreList"
            }
        }
    });
}