$(function(){
	$('#nav a').hover(function(){
		$(this).stop().animate({backgroundColor: "#99CC00"}, 300);
	}, function(){
		var menuItem = $(this);

		if(!menuItem.hasClass("selected")){
			$(this).stop().animate({backgroundColor: "#333"}, 200);	
		}
	});
});
