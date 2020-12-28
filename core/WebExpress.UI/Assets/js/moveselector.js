function MoveSelector(element, id, options, selected)
{
	var DOMParent = document.querySelector(element);
	var DOMSelectedList = DOMParent.firstElementChild;
	var DOMSelectedListHeader = DOMSelectedList.firstElementChild;
	var DOMAvailableList = DOMParent.lastElementChild;
	var DOMAvailableListHeader = DOMAvailableList.firstElementChild;
	var DOMLeftAllButton = DOMParent.children[1].children[0];
    var DOMLeftButton = DOMParent.children[1].children[1];
    var DOMRightButton = DOMParent.children[1].children[2];
    var DOMRightAllButton = DOMParent.children[1].children[3];
	var hidden;
	var selectLeft;
	var selectRight;
	
	function DOMCreate()
	{
		hidden = document.createElement('input');
		hidden.setAttribute("type", "hidden");
		hidden.setAttribute("name", id);

		DOMParent.appendChild(hidden);

		DOMLeftAllButton.addEventListener('click', function()
		{	
			moveAllToLeft();

			return false;
		});

		DOMLeftButton.addEventListener('click', function()
		{	
			moveToLeft(selectRight);

			return false;
		});
		
		DOMRightAllButton.addEventListener('click', function()
		{	
			moveAllToRight();

			return false;
		});

		DOMRightButton.addEventListener('click', function()
		{	
			moveToRight(selectLeft);

			return false;
		});
	}

	function DOMRender()
	{
		DOMSelectedList.innerHTML = '';
		DOMAvailableList.innerHTML = '';
		DOMSelectedList.appendChild(DOMSelectedListHeader);
		DOMAvailableList.appendChild(DOMAvailableListHeader);
		
		var comparison = (a, b) => a === b.id;
		var relativeComplement = options.filter(b => selected.every(a => !comparison(a, b)));
		var intersection = options.filter(b => selected.includes(b.id));

		if (relativeComplement.length == 0)
		{
			DOMLeftAllButton.classList.add("disabled");
			DOMLeftAllButton.disabled = true;
		}
		else
		{
			DOMLeftAllButton.classList.remove("disabled");
			DOMLeftAllButton.disabled = false;
		}

		if (selectRight == null)
		{
			DOMLeftButton.classList.add("disabled");
			DOMLeftButton.disabled = true;
		}
		else
		{
			DOMLeftButton.classList.remove("disabled");
			DOMLeftButton.disabled = false;
		}

		if (selected.length == 0)
		{
			DOMRightAllButton.classList.add("disabled");
			DOMRightAllButton.disabled = true;
		}
		else
		{
			DOMRightAllButton.classList.remove("disabled");
			DOMRightAllButton.disabled = false;
		}

		if (selectLeft == null)
		{
			DOMRightButton.classList.add("disabled");
			DOMRightButton.disabled = true;
		}
		else
		{
			DOMRightButton.classList.remove("disabled");
			DOMRightButton.disabled = false;
		}

		intersection.forEach
		(
			function(currentValue, index)
			{	
				var li = document.createElement('li');
				li.classList.add("list-group-item");
				li.innerHTML = "".concat(currentValue.value);
				li.addEventListener('dblclick', function()
				{	
					moveToRight(currentValue);
					return false;
				});
				li.addEventListener('click', function()
				{	
					selectLeft = currentValue;
					selectRight = null;
					DOMRender();

					return false;
				});

				if (currentValue == selectLeft)
				{
					li.classList.add("bg-primary");
				}

				DOMSelectedList.appendChild(li);
			}
		);

		relativeComplement.forEach
		(
			function(currentValue, index)
			{	
				var li = document.createElement('li');
				li.classList.add("list-group-item");
				li.innerHTML = "".concat(currentValue.value);
				li.addEventListener('dblclick', function()
				{	
					moveToLeft(currentValue);
					return false;
				});
				li.addEventListener('click', function()
				{	
					selectRight = currentValue;
					selectLeft = null;
					DOMRender();

					return false;
				});

				if (currentValue == selectRight)
				{
					li.classList.add("bg-primary");
				}

				DOMAvailableList.appendChild(li);
			}
		);

		hidden.setAttribute("value", selected.map(element => element).join(';'));
	}

	function moveAllToLeft()
	{
		var comparison = (a, b) => a === b.id;
		var relativeComplement = options.filter(b => selected.every(a => !comparison(a, b)));

		selected = selected.concat(relativeComplement.map(element => element.id));

		selectLeft = null;
		selectRight = null;

		DOMRender();
	}

	function moveToLeft(item)
	{
		if (item != null)
		{
			selected = selected.concat(item.id);

			selectLeft = null;
			selectRight = null;

			DOMRender();
		}
	}

	function moveAllToRight()
	{
		selected.length = 0;

		selectLeft = null;
		selectRight = null;

		DOMRender();
	}

	function moveToRight(item)
	{
		if (item != null)
		{
			selected = selected.filter(i => i !== item.id);

			selectLeft = null;
			selectRight = null;

			DOMRender();
		}
	}

	function onKeyUp()
	{
	/*
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
		});*/
	}
	
	function onDelete(id)
	{
	/*
		arrayOfList = arrayOfList.filter(function(currentValue, index)
		{
			if(index == id)
			{
				return false;
			}

			return currentValue;
		});
		
		DOMRender();
		*/
	}

	DOMCreate();
	DOMRender();
	onKeyUp();
}