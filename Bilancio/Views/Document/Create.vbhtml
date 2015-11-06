@ModelType Bilancio.Models.Document

<h2>@ViewBag.Title</h2>

@section Scripts

    @*This is for jquery UI, for Calender control*@
    @Scripts.Render("~/bundles/jqueryui")
    @Styles.Render("~/Content/themes/base/css")

    @*This is for JSON*@
    <script src="~/Scripts/json2.js" type="text/javascript"></script>

    @*These are for styling Control*@
    <link rel="stylesheet" type="text/css" href="~/Scripts/DataTables-1.10.9/css/jquery.dataTables.css" />

    <style type="text/css">
        .cal {
            text-align: center;
        }

        .ral {
            text-align: right;
        }
    </style>

    @*These are for DataTables*@
    <script type="text/javascript" src="~/Scripts/DataTables-1.10.9/js/jquery.dataTables.js"></script>

    <script type="text/javascript">

    var DARE = 'Dare';
    var AVERE= 'Avere';

    function DeleteRow() {
        //sostituito da inner function
    }

    var TABLE_ROW = {
        ID:{pos: 0, head: "ID"},
        Riga:{pos: 1, head: "Riga"},
        Conto_ID:{pos: 2, head: "Conto_ID"},
        Conto:{pos: 3, head: "Conto"},
        Importo:{pos: 4, head: "Importo"},
        Dare:{pos: 5, head: "Dare"},
        Nota:{pos: 6, head: "Nota"}
    };

    $(document).ready(
        function () {

            //table options define
            var table = $('#tblDocLine').DataTable(
                {
                    "bLengthChange": false,
                    "bFilter": false,
                    "bSort": true,
                    "bInfo": true,
                    //"columnDefs": [{ "targets": [TABLE_ROW.ID.pos, TABLE_ROW.Conto_ID.pos], "visible": false, "searchable": false }],
                    "aoColumnDefs": [
                        {bVisible:false, "bSearchable": false, "aTargets": [TABLE_ROW.ID.pos, TABLE_ROW.Conto_ID.pos ]},
                       { "sClass": "cal", "sWidth": "2%", "aTargets": [TABLE_ROW.Riga.pos,TABLE_ROW.Dare.pos ] },
                       { "sClass": "ral", "aTargets": [TABLE_ROW.Importo.pos ] },
                       { "sWidth": "30%", "aTargets": [TABLE_ROW.Nota.pos ] }
                      ],
                    "language": {
                        "decimal": ",",
                        "thousands": ".",
                        "lengthMenu": "_MENU_ righe per pagina",
                        "zeroRecords": "Non ci sono righe",
                        "info": "pagina _PAGE_ di _PAGES_",
                        "infoEmpty": "Non ci sono righe disponibili",
                        "emptyTable": "Tabella vuota",
                        "infoFiltered": "(filtrati su _MAX_ righe totali)",
                        "loadingRecords": "Sto caricando...",
                        "processing": "Sto elaborando...",
                        "search": "Cerca:",
                        "paginate": {
                            "first": "Primo",
                            "last": "Ultimo",
                            "next": "Successivo",
                            "previous": "Precedente"
                        },
                        "aria": {
                            "sortAscending": ": ordine ascendente",
                            "sortDescending": ": ordine discendente"
                        }
                    }
                }
            );

            //select/unselect one single row
            $('#tblDocLine tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            //remove one row from table
            $('#btnDelRow').click(function () {
                // prende i valori della riga selezionata e li assegna ai campi editabili prima di fare la delete della row, in modo che possa funzionare da update della riga
                var rowTable = table.row('.selected')

                assRow(rowTable, false)
            });

            //remove one row from table
            $('#btnUpdRow').click(function () {
                // prende i valori della riga selezionata (compreso l'id di riga) e li assegna ai campi editabili prima di fare la delete della row, in modo che possa funzionare da update della riga
                var rowTable = table.row('.selected')

                assRow(rowTable, true)
            });

            //add one row to table
            $('#btnAddRow').click(function () {
                //todo: prendere i valori della riga ed assegnarli ai campi riga prima di fare la delete, in modo che possa funzionare da update della riga

                var contoId = $('#Conto option:selected').val();
                if (!contoId) {
                    alert("Il conto è obbligatorio");
                    return false;
                }
                
                var amount = Number($('#amountLine').val());
                if (amount == NaN || amount == 0) {
                    alert("L'importo è obbligatorio o importo non corretto");
                    return false;
                }



                var lineId = Number($('#LineId').val())
//                var da = lineId <= 0 ? getDareAvere(contoId) : $('#debit').val(); //solo per le nuove righe il dare avere è definito dal conto
                var da = getDareAvere(contoId);      //il dare avere è definito dal conto

                // Adding item to table
                table.row.add([
                    $('#LineId').val(),
                    $('#rowNr').val(),
                    $('#Conto option:selected').val(),
                    $('#Conto option:selected').text(),
                    $('#amountLine').val(),
                    da,
                    $('#notaLine').val()
                ]).draw(false);

                // Making Editable text empty
                $('#LineId').val("")
                $('#rowNr').val("")
                $('#debit').val("")
                //$('#DebitLabel').text("")
                $('#Conto option:selected').attr("selected", null);
                $('#contoLabel').text("Conto");
                $('#amountLine').val("")
                $('#notaLine').val("")
            });

            // $.datepicker.setDefaults($.datepicker.regional['it']);

            $('#dateReg').datepicker({
                dateFormat: "dd/mm/yy"
                //, yearRange: "2014:2015"
                //, regional:'it'
            });

            $('#dateDoc').datepicker({
                dateFormat: "dd/mm/yy"
                //, yearRange: "2014:2015"
                //, regional: 'it'
            });

        }
    );

    function assRow(rowTable, isUpdate) {

        if (isUpdate) {
            $('#LineId').val(rowTable.data()[TABLE_ROW.ID.pos])
        }
        else {
            $('#LineId').val("")
        }

        var contoID = rowTable.data()[TABLE_ROW.Conto_ID.pos]
        var accountSign = selectByValue(contoID)                    //segno del conto
        var rowSign = rowTable.data()[TABLE_ROW.Dare.pos]           //segno della riga


        var currency = rowTable.data()[TABLE_ROW.Importo.pos]       //importo di tipo currency formattato con la virgola come decimale
//        var amt = Number(currency.replace(",", ".").replace(/[^0-9\.]+/g,""));        //importo della riga

        if (accountSign != rowSign)
            currency = "-" + currency      //il segno dare avere è sempre preso dal conto, quindi se il segno della riga è diverso dal segno del conto l'importo va messo in negativo e in fase di save, l'importo viene forzato in positivo ed il segno invertito

        $('#debit').val(rowSign)
        //$('#DebitLabel').text(rowSign)

        $('#rowNr').val(rowTable.data()[TABLE_ROW.Riga.pos])
        $('#amountLine').val(currency)
        $('#notaLine').val(rowTable.data()[TABLE_ROW.Nota.pos])
        rowTable.remove().draw(false);
    }

        /**
        *imposta selected=true x la option con value txt e visualizza la label del conto con la specifica di conto dare o avere
        */
    function selectByValue(contoID) {

            //Le seguenti sono tutte funzionanti
            //$('#Conto option[value="' + contoID + '"]').attr('selected', true);

            //$('#Conto option')
            //    .filter(function () { return $.trim($(this).val()) == contoID; })
            //    .attr('selected', true);

            $('#Conto option')
               .filter(function (i, e) { return $.trim($(e).val()) == contoID; })
               .attr('selected', true);

            return labelDareAvereConto(contoID);            //  dare/avere del conto
        }

        /**
        * ritorna true se il conto è dare, false se è avere, undefined per altro
        */
        function labelDareAvereConto(contoId) {
            var debit = getDareAvere(contoId)
            $('#contoLabel').text(debit == undefined ? "Conto" : "Conto " + debit);
            return debit
        }

        function changeConto(contoId) {
            var debit = labelDareAvereConto(contoId)
            $('#debit').val(debit)
            //$('#DebitLabel').text(debit)
        }

        /*
        * param id conto id
        * return la stringa Dare o Avere in base all'id passato, può tornare undefinied se l'id non è numerico
        */
        function getDareAvere(id) {
            if (Number(id) <= 0)
                return undefined

            return $.inArray(Number(id), @Html.Raw(Json.Encode(ViewBag.ListAvereAccountChartId))) < 0 ? DARE : AVERE;
        }

        // this function is used to add item to list table
        function Add() {
            //sostituito da inner function
        }

        //This function is used for sending data(JSON Data) to Controller
        function Document_save() {

            // Step 1: Read View Data and Create JSON Object

            // Creating row Json Object
            var row = { "ID": "", "Document_ID": "", "AccountChart_ID": "", "rowNr": "", "debit": "", "amount": "", "note": "" };
            // Creating head Json Object
            var head = { "ID": "", "DocumentType_ID": "", "dateReg": "", "dateDoc": "", "docNr": "", "amount": "", "note": "", "documentRows": [] };

            // Set Sales Main Value
            head.ID = $("#DocumentId").val();
            head.dateReg = $("#dateReg").val();
            head.dateDoc = $("#dateDoc").val();
            head.docNr = $("#docNr").val();
            head.note = $("#note").val();
            head.amount = $("#amount").val();
            head.DocumentType_ID = $("#DocumentType_ID").val();

            var headId = typeof head.ID == "undefined" ? 0 : head.ID;

            // Getting Table Data from where we will fetch Sales Sub Record
            var rowsTable = $('#tblDocLine').DataTable();
            var data = rowsTable.rows().data()              //matrice di dati

            for (var i = 0; i < data.length; i++) {

                row.Document_ID = headId;
                // Set SalesSub individual Value
                row.ID = data[i][TABLE_ROW.ID.pos];
                row.rowNr = data[i][TABLE_ROW.Riga.pos];
//                row.debit = (data[i][TABLE_ROW.Dare.pos] == "Avere" ? -1 : 1);
                row.debit = data[i][TABLE_ROW.Dare.pos];
                row.AccountChart_ID = data[i][TABLE_ROW.Conto_ID.pos];
                row.amount = data[i][TABLE_ROW.Importo.pos];
                row.note = data[i][TABLE_ROW.Nota.pos];

                // adding to head.documentRows List row
                head.documentRows.push(row);
                row = { "ID": "", "Document_ID": "", "AccountChart_ID": "", "rowNr": "", "debit": "", "amount": "", "note": "" };
            }
            // Step 1: Ends Here

            // Set 2: Ajax Post
            // Here i have used ajax post for saving/updating information

//            alert("dati trasmessi al server: " + JSON.stringify(head))

            $.ajax({
                url: '/Document/Create',
                data: JSON.stringify(head),
                type: 'POST',
                contentType: 'application/json;',
                dataType: 'json',
                //beforeSend: function(jqXHR, settings) {
                //    jqXHR.url = settings.url;
                //    alert("beforeSend: " + jqXHR.url +  "  " + settings.type);
                //},
                success: function (result) {
                    if (result.Success == "1") {
                        window.location.href = "/Document/index";
                    }
                    else {
                        alert("error: " + result.ex);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.statusText + " (ajax error) url: " +  jqXHR.url );
                }
            });


        }

</script>

    <script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
end section


@*<form method="post">
    <fieldset>
        <legend>Anagrafica</legend>
        <div class="controls-row">
            @Html.TextBoxFor(Function(model) model.dateReg, New With {.class = "span1", .label = "Data Reg.", .placeholder = "Data Reg."})
            @Html.TextBoxFor(Function(model) model.dateDoc, New With {.class = "span1", .label = "Data Doc.", .placeholder = "Data Documento"})
            @Html.TextBoxFor(Function(model) model.docNr, New With {.class = "span1", .label = "Nr. Doc.", .placeholder = "Nr. doc."})
            @Html.TextBoxFor(Function(model) model.amount, New With {.class = "span1", .label = "Tot. Doc.", .placeholder = "Tot. Documento"})
        </div>
        <div class="controls-row">
            @Html.TextBoxFor(Function(model) model.note, New With {.class = "span2", .placeholder = "Note"})
        </div>
    </fieldset>
</form>*@


@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Testata Documento</legend>

        @If (Not IsNothing(Model) AndAlso Model.ID <> 0) Then
            @<input type="hidden" id="DocumentId" name="DocumentId" value="@Model.ID" />
        End If

         @*<div class="controls-row">*@
        <div class="editor-label">
            @Html.LabelFor(Function(model) model.DocumentType_ID)
        </div>
        <div class="editor-field">
            @Html.DropDownListFor(Function(x) x.DocumentType_ID, CType(ViewBag.documentTypes, IEnumerable(Of SelectListItem)))
            @*@Html.DropDownListFor(Function(x) x.DocumentType_ID, CType(ViewBag.documentTypes, IEnumerable(Of SelectListItem)), New With {.class = "span4", .placeholder = "Tipo Doc."})*@
            @Html.ValidationMessageFor(Function(model) model.DocumentType_ID)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateReg)
        </div>
        <div class="editor-field">
            @Html.TextBoxFor(Function(model) model.dateReg)
            @*@Html.EditorFor(Function(model) model.dateReg, New With {.class = "span1", .placeholder = "Data Registrazione"})*@
            @Html.ValidationMessageFor(Function(model) model.dateReg)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateDoc)
        </div>
        <div class="editor-field">
            @Html.TextBoxFor(Function(model) model.dateDoc)    @*, New With {.class = "span1", .placeholder = "Data Documento"})*@
            @Html.ValidationMessageFor(Function(model) model.dateDoc)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.docNr)
            </div>
        <div class="editor-field">
            @Html.TextBoxFor(Function(model) model.docNr)  @*, New With {.class = "span1", .placeholder = "Nr. Documento"})*@
            @Html.ValidationMessageFor(Function(model) model.docNr)
        </div>
         @*</div>

         <div class="controls-row">*@
        <div class="editor-label">
            @Html.LabelFor(Function(model) model.note)
        </div>
        <div class="editor-field">
            @*@Html.EditorFor(Function(model) model.note, New With {.class = "textarea"})*@
            @Html.TextAreaFor(Function(model) model.note)  @*, New With {.class = "span2", .placeholder = "Note"})*@
            @Html.ValidationMessageFor(Function(model) model.note)
        </div>

         <div style="float: right; margin: 5px;">
             <div class="editor-label">
                 @Html.LabelFor(Function(model) model.amount)
             </div>
             <div class="editor-field">
                 @Html.TextBoxFor(Function(model) model.amount)     @*, New With {.class = "span3", .placeholder = "Tot. Documento"})*@
                 @Html.ValidationMessageFor(Function(model) model.amount)
             </div>

         </div>

         @*</div>*@

</fieldset>

    @<br />
    @<fieldset>
        <legend>Righe Documento</legend>

        <input type="hidden" id="LineId" name="LineId" value="" />

        <label>riga :</label>
        @Html.TextBox("rowNr", Nothing, New With {.style = "width:5%"})

        @*<label id="Debit"></label>*@
@*        @Html.CheckBox("debit", False)*@

        <label id="contoLabel">Conto :</label>
        @Html.DropDownList("Conto", New SelectList(ViewBag.ListChart, "ID", "Name"), "-- scegliere un conto", New With {.onchange = "changeConto($(this).val());"})

        <label>importo :</label>
        @Html.TextBox("amountLine")

        @*<label id="DebitLabel"></label>*@
        <input type="hidden" id="debit" name="debit" value="" />

        <br />
        <label>nota :</label>
        @Html.TextBox("notaLine", Nothing, New With {.style = "width:70%"})

         <input type="button" id="btnAddRow" value="Aggiungi" class="btn" />
        <br />
        <br />

        <table class="display" id="tblDocLine">
            <thead><tr> <th>ID</th> <th>Riga</th> <th>Conto_ID</th> <th>Conto</th> <th>Importo</th> <th>Dare</th> <th>Nota</th></tr></thead>
            <tbody>
                @If (Not IsNothing(Model) AndAlso Not IsNothing(Model.documentRows)) Then
                        
                    @For Each item In Model.documentRows.OrderBy(Function(a) a)
                        @<tr>  
                            <td>@Html.DisplayTextFor(Function(i) item.ID)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.rowNr)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.AccountChart_ID)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.AccountChart.Name)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.amount)</td>  
                            <td>@Html.DisplayTextFor(Function(i) item.debit)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.note)</td>
                        </tr>
                    Next
                End If
            </tbody>
        </table>

        <br />

        <input type="button" id="btnDelRow" value="Elimina riga selezionata" class="btn" />
        <input type="button" id="btnUpdRow" value="Modifica riga selezionata" class="btn" />

    </fieldset>

    @<input type="button" value="Salva Documento" onclick="Document_save()" class="btn" />

End Using

<div>    @Html.ActionLink("Vai alla lista", "Index")  </div>
