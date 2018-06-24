// function OpenCloseMenuChildMenu(obj)
// {
//     if ($(obj).parent().find("ul:first").is(':visible'))
//     {
//         $(obj).parent().find("ul:first").hide();
//     }
//     else
//     {
//         $(obj).parent().find("ul:first").show();
//     }
// }

// function SideMenuOpenClose() {
//     if ($("body").hasClass('sidebar-collapse'))
//     {
//         $("body").removeClass('sidebar-collapse');
//     }
//     else
//     {
//         $("body").addClass('sidebar-collapse');
//     }
// }
function setBodyClass(val)
{
    if (val == 1)
    {
        $("body").removeClass("hold-transition");
        $("body").addClass("skin-blue sidebar-mini");
    }
}