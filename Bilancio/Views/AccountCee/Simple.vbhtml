﻿
@ModelType IEnumerable(Of Bilancio.Models.AccountCee)


@Code
    ViewData("Title") = "Piano dei conti cee"
End Code

<style>
    /*Here We will add some css for style our treeview*/
    .collapse {
        width: 15px;
        background-image: url('../Images/ui-icons_454545_256x240.png');
        background-repeat: no-repeat;
        background-position: -36px -17px;
        display: inline-block;
        cursor: pointer;
    }

    .expand {
        width: 15px;
        background-image: url('../Images/ui-icons_454545_256x240.png');
        background-repeat: no-repeat;
        background-position: -50px -17px;
        display: inline-block;
        cursor: pointer;
    }

    .treeview ul {
        font: 14px Arial, Sans-Serif;
        margin: 0px;
        padding-left: 20px;
        list-style: none;
    }

    .treeview > li > a {
        font-weight: bold;
    }

    .treeview li {
    }

        .treeview li a {
            padding: 4px;
            font-size: 12px;
            display: inline-block;
            text-decoration: none;
            width: auto;
        }
</style>

<h2>Albero del piano dei conti cee</h2>
<div style="border:solid 1px black; padding:10px; background-color:#FAFAFA">
    <div class="treeview">
        @If (Not Model Is Nothing And Model.Count > 0) Then
            @<ul>
                @TreeView.GetTreeView(Model, Model.FirstOrDefault.ID)
            </ul>
        End If
    </div>
</div>

@* Here We need some Jquery code for make this treeview collapsible *@
@section Scripts
    <script>
        $(document).ready(function () {
            $(".treeview li>ul").css('display', 'none'); // Hide all 2-level ul
            $(".collapsible").click(function (e) {
                e.preventDefault();
                $(this).toggleClass("collapse expand");
                $(this).closest('li').children('ul').slideToggle();
            });
        });
    </script>
end section
