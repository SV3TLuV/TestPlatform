import {ReactNode} from "react";

interface ICenteredProps {
    children: ReactNode
}

export const Centered = ({children}: ICenteredProps) => {
    return (
        <div style={{
            display: "flex",
            placeItems: "center",
            minHeight: "calc(100vh - 80px)",
            minWidth: "240px"
        }}>
            {children}
        </div>
    )
}