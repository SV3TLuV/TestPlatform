import {useDroppable} from "@dnd-kit/core";

interface IDroppableProps {
    id: string
    children: any
}

export const Droppable = ({id, children}: IDroppableProps) => {
    const {isOver, setNodeRef} = useDroppable({
        id: id,
    });
    const style = {
        opacity: isOver ? 1 : 0.5,
    };

    return (
        <div ref={setNodeRef} style={style}>
            {children}
        </div>
    );
}