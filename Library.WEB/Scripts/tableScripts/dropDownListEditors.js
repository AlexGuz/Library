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
                    read: "AutorsList"
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
                    read: "GenreList"
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
                    read: "GenreList"
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
                    read: "GenreList"
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
                    read: "GenreList"
            }
        }
    });
}