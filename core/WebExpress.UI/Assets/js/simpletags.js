/**
 * Base on https://github.com/kurtobando/simple-tags
 */
function Tags(element, id, listOfTags)
{
	var arrayOfList = listOfTags;
	var DOMParent = document.querySelector(element);
	var DOMList;
	var DOMInput;
	var hidden;
	
	function DOMCreate()
	{
		var ul = document.createElement('ul');
		var li = document.createElement('li');
		var input = document.createElement('input');
		
		hidden = document.createElement('input');
		hidden.setAttribute("type", "hidden");
		hidden.setAttribute("name", id);
		
		DOMParent.appendChild(ul);
		DOMParent.appendChild(hidden);
		DOMParent.appendChild(input);
		DOMList = DOMParent.firstElementChild;
		DOMInput = DOMParent.lastElementChild;
	}

	function DOMRender()
	{
		DOMList.innerHTML = '';
		arrayOfList.forEach
		(
			function(currentValue, index)
			{	
				var li=document.createElement('li');
				li.innerHTML = "".concat(currentValue.toLowerCase(), "<a>&times;</a>");
				li.querySelector('a').addEventListener('click', function()
				{	
					onDelete(index);
					
					return false;
				});
				
				DOMList.appendChild(li);
			}
		);

		hidden.setAttribute("value", arrayOfList.map(element => element.toLowerCase()).join(';'));
	}

	function onKeyUp()
	{
		DOMInput.addEventListener('keyup', function(event)
		{
			var text = this.value;
			if(text.endsWith(',') || text.endsWith(';') || text.endsWith(' '))
			{
				var replace = text.replace(',', '').replace(';', '').replace(' ', '').toLowerCase();
				if(replace != '')
				{
					arrayOfList.push(replace);
				}

				this.value = '';
			}

			DOMRender();
		});
	}
	
	function onDelete(id)
	{
		arrayOfList = arrayOfList.filter(function(currentValue, index)
		{
			if(index == id)
			{
				return false;
			}

			return currentValue;
		});
		
		DOMRender();
	}

	DOMCreate();
	DOMRender();
	onKeyUp();
}