import {useState} from "react";

export const useDialog = (showed: boolean = false) => {
    const [open, setOpen] = useState(showed);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    return {open, handleOpen, handleClose};
}