import {useDraggable} from "@dnd-kit/core";
import {CSS} from "@dnd-kit/utilities";


interface IDraggableProps {
    id: string
    children: any
}

export const Draggable = ({id, children}: IDraggableProps) => {
    const {attributes, listeners, setNodeRef, transform} = useDraggable({
        id: id,
    });

    const style = {
        // Outputs `translate3d(x, y, 0)`
        transform: CSS.Translate.toString(transform),
    };

    return (
        <div ref={setNodeRef} style={style} {...listeners} {...attributes}>
            {children}
        </div>
    )
}
