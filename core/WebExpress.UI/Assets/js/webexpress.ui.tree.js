/**
 * Baum
 * Folgende Events werden ausgelöst:
 * - webexpress.ui.change.visibility 
 */
webexpress.ui.treeCtrl = class extends webexpress.ui.events {
    _container = $("<ul class='tree'/>");
    _nodes = [];

    /**
     * Constructor
     * @param settings Optionen zur Gestaltung des Steuerelementes
     *        - id Die ID des Steuerelements
     *        - css CSS-Klasse zur Gestaltung des Steuerelementes
     */
    constructor(settings) {
        super();

        let id = settings.id ?? "";
        let css = settings.css;

        this._container.attr("id", id);
        this._container.addClass(css);
    }
    
    /**
     * Aktualisierung des Steuerelementes.
     * Diese Funktion arbeitet rekursiv.
     * @param container Das Stuerelement
     * @param nodes Die einzufügenden Knoten
     */
    update(container, nodes) {
        container.children().remove();
        
        nodes.forEach(function (node) {
            let children = node.children;
            let color = node.color ?? "";
            let li = $("<li class='tree-node'></li>");
            let expand = $("<span class='" + color + "'/>");
            let icon = $("<i class='" + (node.icon ?? "") + "'/>");
            li.append(expand);
                        
            if (children != null) {
                let ul = $("<ul/>");
                let indicator = $("<a class='tree-indicator-angle' href='#'/>");
                if (node.expand) {
                    this.update(ul, children);
                    indicator.addClass("tree-expand");
                }
                expand.append(indicator);
                li.append(ul);
                
                expand.click(function () {
                    this.trigger('webexpress.ui.change.visibility', li, node);      
                    if (!node.expand) {
                        indicator.addClass("tree-expand");
                        this.update(ul, children);
                    } else {
                        indicator.removeClass("tree-expand");
                        ul.children().remove();
                    }
                    node.expand = !node.expand;
                }.bind(this));
            } else {
                let indicator = $("<a class='tree-indicator-dot' href='#'/>");
                expand.append(indicator);
            }
            
            if (node.icon != null) {
                expand.append(icon);
            }
            
            if (node.render != null && (typeof node.render === 'string' || node.render instanceof String)) {
                let render = Function("node", node.render);
                let renderResult = render(node);
                if (renderResult != null && renderResult != null) {
                    expand.append(renderResult);
                }
            } else {
                
                expand.append($("<span>" + node.label + "</span>"));
            }
            
            container.append(li);
            
        }.bind(this));
    }
    
    /**
     * Gibt die Baumknoten zurück
     */
    get nodes() {
        return this._nodes;
    }
    
    /**
     * Setzt die Knoten des Baumes
     * @param nodes Ein Array mit ObjektIDs {id, label, description, image, color, expand}
     */
    set nodes(nodes) {
        this._nodes = nodes;
          
        this.update(this._container, this._nodes);
    }

    /**
     * Löscht alle Einträge aus dem Baum
     */
    clear() {
        this._container.children().remove();
    }

    /**
     * Gibt das Steuerelement zurück
     */
    get getCtrl() {
        return this._container;
    }
}