(function($)
{
    $.fn.input = function ()
    {
        return this.each(function ()
        {
            $(this).addClass("ui-widget ui-widget-content ui-corner-all");
            $(this).css({ "height": "26px" });
        });
    }
})(jQuery);