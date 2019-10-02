depth = 0;
finid = '';
curid = 7853368458912384945;

speed = 3000;
myid = new Array(); 
i = 0;

go = 0;
checkme = 0;

function hasclassdown(id){

	myid[id] = id;
	idl = '#'+id;
	clss = jQuery(idl).attr('class');
	
	if(clss){
	
		newlook = clss.substr(7);
		hasclassdown('node-'+newlook);
	
	}


}


function checkfinished(myid){

		jQuery('#menus_container input').each(function(){
	
			thisname = jQuery(this).attr('id');

				if(!myid[thisname] && thisname.length > 0){
			
					jQuery(this).parent().parent().find('li').hide();
					//jQuery(this).parent().removeClass('selected');

				}else{

					jQuery(this).parent().parent().find('li').show();

					if(checkme == 0){

					jQuery(this).parent().find('label').eq(0).removeClass('arrow_left');
					jQuery(this).parent().find('label').eq(0).addClass('arrow_down');

					}

				}


	});
checkme = 1;
}


function checkall(){

jQuery('#menus_container input').each(function(){

		valch = jQuery(this).attr('checked');

			thisname = jQuery(this).attr('id');

			if(valch == true && checkme == 0){

				jQuery(this).parent().parent().find('li').show();

				jQuery(this).parent().addClass('selected');

			
				myid[thisname] = thisname;
				hasclassdown(thisname);

			}else{

			

			}
	
		
	});


checkfinished(myid);


}

function removechecked(id){


	jQuery(id).attr('checked', false);



}

function addEvenOdd(th){



	

}


i = 0;


jQuery(document).ready(function(){








	jQuery('#menus_container ul li ul').each(function(){
	
	
		jQuery(this).parent().find('label').eq(0).addClass('children');
		jQuery(this).parent().find('label').eq(0).addClass('arrow_left');
		jQuery(this).parent().find('p').eq(0).addClass('children_p');


	});

	jQuery('#menus_container li').each(function(){


		forlabel = jQuery(this).find('input').eq(0).attr('id');

	

		jQuery(this).find('label').attr('title',forlabel);


	});



	checkall();

	jQuery('input').click(function(){

		valch = jQuery(this).attr('checked');

		if(valch){

		  jQuery(this).parent().addClass('selected');

		  }else{

		  jQuery(this).parent().removeClass('selected');
		

		}

		//myid = new Array(); 
		//checkall();

	})


	jQuery('#menus_container label').click(function(){

		
		inputid = jQuery(this).attr('title');

		if(!myid[inputid]){

			myid[inputid] = inputid;
			checkall();

		}else{

			delete myid[inputid];
			checkall();

		}
	

	});


	jQuery('ul li p').hover(function(){

		bgcol = jQuery(this).css('background-color');

		jQuery(this).stop().animate({backgroundColor: "#D9FF66"}, 300);

	},function() {
					
	


			if(jQuery(this).parent().parent().parent().attr('id') == 'menus_container'){

				if(jQuery(this).hasClass('selected')){

					jQuery(this).stop().css('background-color','#99CC00');
	
				}else{
	
					jQuery(this).stop().css('background-color','#ffffff');
	
				}

			}else{

				if(jQuery(this).hasClass('selected')){

					jQuery(this).stop().css('background-color','#99CC00');
	
				}else{
	
					jQuery(this).stop().css('background-color','#E9E9E9');
	
				}
			}

	


	});	


		jQuery('label.children').toggle(function(e){
		
				if(jQuery(this).hasClass('arrow_left')){

						jQuery(this).removeClass('arrow_left');
						jQuery(this).addClass('arrow_down');
	
				}else{

						jQuery(this).removeClass('arrow_down');
						jQuery(this).addClass('arrow_left');

				}


			},function(){

				if(jQuery(this).hasClass('arrow_left')){

						jQuery(this).removeClass('arrow_left');
						jQuery(this).addClass('arrow_down');
	
				}else{

						jQuery(this).removeClass('arrow_down');
						jQuery(this).addClass('arrow_left');

				}
	

		});
		







});

