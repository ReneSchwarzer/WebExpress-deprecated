/**
 * Auswahlfeld zum Aktivieren von Optionen
 * - webexpress.ui.change.value mit Parameter value
 */
webexpress.ui.moveCtrl = class extends webexpress.ui.events {
    _container = $("<div class='move'/>");
    _selectedList = $("<ul class='list-group list-group-flush'/>");
    _availableList = $("<ul class='list-group list-group-flush'/>");
    _buttonToSelectedAll = $("<button class='btn btn-primary btn-block' type='button'/>");
    _buttonToSelected = $("<button class='btn btn-primary btn-block' type='button'/>");
    _buttonToAvailable = $("<button class='btn btn-primary btn-block' type='button'/>");
    _buttonToAvailableAll = $("<button class='btn btn-primary btn-block' type='button'/>");
    _hidden = $("<input type='hidden'/>");
    _options = [];
    _values = [];
    _selectedoptions = new Map(); // Key=Ctrl, Value=options
    _availableoptions = new Map(); // Key=Ctrl, Value=options
    
    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die Id des Steuerelements
     *        - name Der Steuerelementenname
     *        - css CSS-Klasse zur Gestaltung des Steuerelementes
     *        - header Überschrift { Selected, Available }
     *        - buttons Schaltflächenbeschriftung { toselectedall, toselected, toavailable, toavailableall }
     */
    constructor(settings) {
        super();

        let id = settings.id;
        let name = settings.name;
        let css = settings.css;
        let header = settings.header;
        let buttons = settings.buttons;
        let selectedContainer = $("<div class='move-list'/>");
        let selectedHeader = $("<span class='text-muted'>" + header.selected + "</span>");
        let availableContainer = $("<div class='move-list'/>");
        let availableHeader = $("<span class='text-muted'>" + header.available + "</span>");
        let buttonContainer = $("<div class='move-button d-grid gap-2'/>");
        
        this._container.attr("id", id ?? "");

        if (css != null) {
            this._container.addClass(css);
        }

        if (name != null) {
            this._hidden.attr("name", name);
        }
        
        this._buttonToSelectedAll.html(buttons.toselectedall);
        this._buttonToSelected.html(buttons.toselected);
        this._buttonToAvailable.html(buttons.toavailable);
        this._buttonToAvailableAll.html(buttons.toavailableall);
        
        selectedContainer.append(selectedHeader);
        selectedContainer.append(this._selectedList);
        availableContainer.append(availableHeader);
        availableContainer.append(this._availableList);
        buttonContainer.append(this._buttonToSelectedAll);
        buttonContainer.append(this._buttonToSelected);
        buttonContainer.append(this._buttonToAvailable);
        buttonContainer.append(this._buttonToAvailableAll);

        selectedContainer.on('dragenter', function(e) {
            e.preventDefault();
            this._selectedList.addClass('drag-over');
        }.bind(this));
        
        selectedContainer.on('dragover', function(e) {
            e.preventDefault();
            this._selectedList.addClass('drag-over');
        }.bind(this));
        
        selectedContainer.on('dragleave', function() {
            this._selectedList.removeClass('drag-over');
        }.bind(this));
        
        selectedContainer.on('drop', function(e) {
            this._selectedList.removeClass('drag-over');
            this.moveToSelected();
            e.preventDefault(); 
        }.bind(this));
        
        availableContainer.on('dragenter', function(e) {
            e.preventDefault();
            this._availableList.addClass('drag-over');
        }.bind(this));
        
        availableContainer.on('dragover', function(e) {
            e.preventDefault();
            this._availableList.addClass('drag-over');
        }.bind(this));
        
        availableContainer.on('dragleave', function() {
            this._availableList.removeClass('drag-over');
        }.bind(this));
        
        availableContainer.on('drop', function(e) {
            this._availableList.removeClass('drag-over');
            this.moveToAvailable();
            e.preventDefault();
        }.bind(this));
        
        this._buttonToSelectedAll.click(function() {    
            this.moveToSelectedAll();
        }.bind(this));

        this._buttonToSelected.click(function() {
            this.moveToSelected();
        }.bind(this));
        
        this._buttonToAvailableAll.click(function() {
            this.moveToAvailableAll();
        }.bind(this));

        this._buttonToAvailable.click(function() {  
            this.moveToAvailable();
        }.bind(this));
        
        this._container.append(selectedContainer);
        this._container.append(buttonContainer);
        this._container.append(availableContainer);

        if (name !== undefined && name != null) {
            this._container.append(this._hidden);
        }
        
        this.update();
    }
    
    /**
     * Verschiebe alle Einträge nach links (selected)
     */
    moveToSelectedAll() {
        this.value = this._options.map(element => element.Id);

        this.update();
    }
    
    /**
     * Verschiebt ein einzelnen Eintrag nach links (selected)
     */
    moveToSelected() {
        this.value = this._values.concat(Array.from(this._availableoptions.values()).filter(elem => elem != null).map(elem => elem.Id));
        
        this.update();
    }

    /**
     * Verschiebe alle Einträge nach rechts (available)
     */
    moveToAvailableAll() {
        this.value = [];

        this.update();
    }

    /**
     * Verschiebt ein einzelnen Eintrag nach rechts (available)
     */
    moveToAvailable() {
        this.value = this._values.filter(b => !Array.from(this._selectedoptions.values()).filter(elem => elem != null).map(elem => elem.Id).includes(b));
        
        this.update();
    }
   
    /**
     * Aktualisierung des Steuerelementes
     */
    update() {
        let values = this._values != null ? this._values : [];
        let comparison = (a, b) => a === b.Id;
        let relativeComplement = this._options.filter(b => values.every(a => !comparison(a, b)));
        let intersection = this._options.filter(b => values.includes(b.Id));
        
        this._selectedList.children().remove();
        this._availableList.children().remove();
        this._selectedoptions.clear();
        this._availableoptions.clear();

        function updateselection() {
            this._selectedoptions.forEach(function(value, key) {
                if (value != null) {
                    key.addClass("bg-primary");
                    key.children().addClass("text-white");
                } else {
                    key.removeClass("bg-primary");
                    key.children().removeClass("text-white");
                }
            });
            this._availableoptions.forEach(function(value, key) {
                if (value != null) {
                    key.addClass("bg-primary");
                    key.children().addClass("text-white");
                } else {
                    key.removeClass("bg-primary");
                    key.children().removeClass("text-white");
                }
            });
            
            if (Array.from(this._availableoptions.values()).filter(elem => elem != null).length == 0) {
                this._buttonToSelected.addClass("disabled");
                this._buttonToSelected.prop("disabled", true);
            } else {
                this._buttonToSelected.removeClass("disabled");
                this._buttonToSelected.prop("disabled", false);
            }
            
            if (Array.from(this._selectedoptions.values()).filter(elem => elem != null).length == 0) {
                this._buttonToAvailable.addClass("disabled");
                this._buttonToAvailable.prop("disabled", true);
            } else {
                this._buttonToAvailable.removeClass("disabled");
                this._buttonToAvailable.prop("disabled", false);
            }
        }

        intersection.forEach(function(currentValue) {   
            let li = $("<li class='list-group-item' draggable='true'/>");
            let img = $("<img title='' src='" + currentValue.image + "' draggable='false'/>");
            let icon = $("<i class='text-primary " + currentValue.icon + "' draggable='false'/>");
            let a = $("<a class='link' href='javascript:void(0)' draggable='false'>" + "".concat(currentValue.label) + "</a>");
            if (currentValue.icon != null) {
                li.append(icon);
            }
            if (currentValue.image != null) {
                li.append(img);
            }
            li.append(a);
            this._selectedoptions.set(li, null);
                        
            li.click(function() {   
                if (event.ctrlKey) {
                    if (!Array.from(this._selectedoptions.values()).some(elem => elem === currentValue)) {
                        this._selectedoptions.set(li, currentValue);
                    } else {
                        this._selectedoptions.set(li, null);
                    }
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                } else {
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                    this._selectedoptions.set(li, currentValue);
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                }
                updateselection.call(this);
            }.bind(this, a)).dblclick(function() {  
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._selectedoptions.set(li, currentValue);
                this._availableoptions.forEach((value, key, map) => map.set(key, null));

                this.moveToAvailable();
            }.bind(this, a)).keyup(function() { 
                if (event.keyCode === 32) {
                    if (!Array.from(this._selectedoptions.keys()).some(elem => elem === currentValue)) {
                        this._selectedoptions.set(li, currentValue);
                    } else {
                        this._selectedoptions.set(li, null);
                    }
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                    updateselection.call(this);
                }
            }.bind(this, a));
            
            li.on('dragstart', function dragStart(e) {
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._selectedoptions.set(li, currentValue);
                this._availableoptions.forEach((value, key, map) => map.set(key, null));    
                updateselection.call(this);             
            }.bind(this));

            this._selectedList.append(li);
        }.bind(this));

        relativeComplement.forEach(function(currentValue) { 
            let li = $("<li class='list-group-item' draggable='true'/>");
            let img = $("<img title='' src='" + currentValue.image + "' draggable='false'/>");
            let icon = $("<i class='text-primary " + currentValue.icon + "' draggable='false'/>");
            let a = $("<a class='link' href='javascript:void(0)' draggable='flase'>" + "".concat(currentValue.label) + "</a>");
            if (currentValue.icon != null) {
                li.append(icon);
            }
            if (currentValue.image != null) {
                li.append(img);
            }
            li.append(a);
            this._availableoptions.set(li, null);
            
            li.click(function() {   
                if (event.ctrlKey) {
                    if (!Array.from(this._availableoptions.values()).some(elem => elem === currentValue)) {
                        this._availableoptions.set(li, currentValue);
                    } else {
                        this._availableoptions.set(li, null);
                    }
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                } else {
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                    this._availableoptions.forEach((value, key, map) => map.set(key, null));
                    this._availableoptions.set(li, currentValue);
                }
                                
                updateselection.call(this);
            }.bind(this, a)).dblclick(function() {  
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.set(li, currentValue);

                this.moveToSelected();
            }.bind(this, a)).keyup(function() { 
                if (event.keyCode === 32) {
                    if (!Array.from(this._availableoptions.keys()).some(elem => elem === currentValue)) {
                        this._availableoptions.set(li, currentValue);
                    } else {
                        this._availableoptions.set(li, null);
                    }
                    this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                    updateselection.call(this);
                }
            }.bind(this, a));
            
            li.on('dragstart', function dragStart(e) {
                this._selectedoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.forEach((value, key, map) => map.set(key, null));
                this._availableoptions.set(li, currentValue);
                updateselection.call(this);
            }.bind(this));

            this._availableList.append(li);
        }.bind(this));
        
        if (relativeComplement.length == 0) {
            this._buttonToSelectedAll.addClass("disabled");
            this._buttonToSelectedAll.prop("disabled", true);
        } else {
            this._buttonToSelectedAll.removeClass("disabled");
            this._buttonToSelectedAll.prop("disabled", false);
        }

        if (values.length == 0) {
            this._buttonToAvailableAll.addClass("disabled");
            this._buttonToAvailableAll.prop("disabled", true);
        } else {
            this._buttonToAvailableAll.removeClass("disabled");
            this._buttonToAvailableAll.prop("disabled", false);
        }
        
        updateselection.call(this);
    }

    /**
     * Gibt alle Optionen zurück
     */
    get options() {
        return this._options;
    }

    /**
     * Setzt die Optionen
     * @param options Ein Array mit Optionen { Id, Label, Icon, Image }
     */
    set options(options) {
        this._options = options;

        this.update();
    }
    
    /**
     * Gibt die ausgewählten Optionen zurück
     */
    get value() {
        return this._values;
    }
    
    /**
     * Setzt die ausgewählten Optionen
     * @param values Ein Array mit ObjektIds
     */
    set value(values) {
        if (this._values != values) {

            this._values = values;
            this._hidden.val(this._values.map(element => element).join(';'));

            this.update();

            this.trigger('webexpress.ui.change.value', values);
        }
    }
    
    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}