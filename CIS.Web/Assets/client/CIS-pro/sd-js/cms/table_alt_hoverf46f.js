$(function(){
	$(".table tr:nth-child(odd)").addClass('table_row_odd');
	
	$(".table tr").hover(function(){
		bgcol = $(this).css('background-color');
			$(this).stop().animate({backgroundColor: "#D9FF66"}, 300);
		}, function() {
			if($(this).hasClass('table_row_odd')){
				$(this).stop().animate({backgroundColor: "#E9E9E9"}, 200);
			}else{
				$(this).stop().animate({backgroundColor: "#FFF"}, 200);
			}
	});	

});


