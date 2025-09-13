import {Button, Typography} from "@mui/material"
import {useNavigate} from "react-router-dom"

export const HomePage = () => {
    const navigate = useNavigate();

    return (
        <>
            <Typography variant="h3" sx={{mb: 2}}>Выберите режим</Typography>
            <Button
                variant="contained"
                onClick={() => navigate("/dictionary")}
                sx={{mr: 2}}
            >
                Заполнить словарь
            </Button>
            <Button
                variant="outlined"
                onClick={() => navigate("/check")}
            >
                Проверить знания
            </Button>
        </>
    )
}