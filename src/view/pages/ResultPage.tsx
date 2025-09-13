import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import MenuBookOutlinedIcon from '@mui/icons-material/MenuBookOutlined';
import CloseOutlinedIcon from '@mui/icons-material/CloseOutlined';
import {useLocation, useNavigate} from "react-router-dom";
import {Box, Button, Card, CardContent, Stack, Typography,} from "@mui/material";

export const ResultPage = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const {correct, incorrect, total} = location.state as {
        correct: number;
        incorrect: number;
        total: number;
    };

    return (
        <Box>
            <Typography variant="h3" sx={{color: "#364963", mb: 2}}>
                Результат проверки знаний
            </Typography>
            <Card sx={{width: 350, mb: 4}}>
                <CardContent>
                    <Typography sx={{color: "#31558a", fontWeight: "700", mb: 2}}>
                        Ответы
                    </Typography>
                    <Stack direction={"row"} sx={{ml: 1, mb: 1, borderBottom: "1px solid #dddddd"}}>
                        <CheckCircleOutlineIcon color="success"/>
                        <Typography gutterBottom ml={1}>
                            Правильные
                        </Typography>
                        <Typography sx={{color: "#364963", fontWeight: "700", ml: "auto"}}>
                            {correct}
                        </Typography>
                    </Stack>
                    <Stack direction={"row"} sx={{ml: 1, mb: 1, borderBottom: "1px solid #dddddd"}}>
                        <CloseOutlinedIcon color="error"/>
                        <Typography gutterBottom ml={1}>
                            Неправильные
                        </Typography>
                        <Typography sx={{color: "#364963", fontWeight: "700", ml: "auto"}}>
                            {incorrect}
                        </Typography>
                    </Stack>
                    <Stack direction={"row"} sx={{ml: 1, mb: 1, borderBottom: "1px solid #dddddd"}}>
                        <MenuBookOutlinedIcon color="secondary"/>
                        <Typography sx={{ml: 1}}>
                            Всего слов
                        </Typography>
                        <Typography sx={{color: "#364963", fontWeight: "700", ml: "auto"}}>
                            {total}
                        </Typography>
                    </Stack>
                </CardContent>
            </Card>
            <Stack direction={"row"}>
                <Button
                    variant="contained"
                    onClick={() => navigate("/check")}
                    sx={{fontWeight: "700", mr: 1, textTransform: "uppercase"}}
                >
                    Проверить знания ещё раз
                </Button>
                <Button
                    variant="outlined"
                    onClick={() => navigate("/")}
                    sx={{textTransform: "uppercase"}}
                >
                    Вернуться в начало
                </Button>
            </Stack>
        </Box>
    );
}