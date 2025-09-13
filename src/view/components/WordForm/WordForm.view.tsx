import {Box, Button, Stack, TextField, Typography} from "@mui/material";
import type {useWordForm} from "./WordForm.state.ts";
import {useState} from "react";
import {useNavigate} from "react-router-dom";

export const WordFormView = ({word, onSubmit}: ReturnType<typeof useWordForm>) => {
    const navigate = useNavigate();
    const [value, setValue] = useState<string>(word?.word ?? "");
    const [translation, setTranslation] = useState<string>(word?.translation ?? "");

    return (
        <Box>
            <Box sx={{border: "2px solid #ededed", mb: 4, background: "#ffffff", borderRadius: 2}}>
                <Box sx={{p: 4, borderBottom: "2px solid #ededed"}}>
                    <Typography variant="h5" sx={{color: "#364963"}}>
                        Словарное слово
                    </Typography>
                </Box>
                <Box component="form" sx={{p: 4}}>
                    <Stack sx={{
                        maxWidth: "650px",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        alignItems: "center",
                        mb: 4
                    }}>
                        <Typography sx={{color: "#364963"}}>
                            Слово на русском языке
                        </Typography>
                        <TextField
                            fullWidth
                            variant="outlined"
                            value={value}
                            error={!value}
                            onChange={(e) => setValue(e.target.value)}
                            required
                            sx={{maxWidth: "250px"}}
                        />
                    </Stack>
                    <Stack sx={{
                        maxWidth: "650px",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        alignItems: "center"
                    }}>
                        <Typography sx={{color: "#364963"}}>
                            Перевод на английский язык
                        </Typography>
                        <TextField
                            fullWidth
                            variant="outlined"
                            value={translation}
                            error={!translation}
                            onChange={(e) => setTranslation(e.target.value)}
                            required
                            sx={{maxWidth: "250px"}}
                        />
                    </Stack>
                </Box>
            </Box>
            <Stack sx={{flexDirection: "row", gap: 2}}>
                <Button
                    variant="contained"
                    disabled={!value || !translation}
                    onClick={() => {
                        onSubmit(value, translation);
                        navigate(-1);
                    }}
                    sx={{textTransform: "uppercase"}}
                >
                    Сохранить
                </Button>
                <Button
                    variant="outlined"
                    onClick={() => navigate(-1)}
                    sx={{textTransform: "uppercase"}}
                >
                    Отменить
                </Button>
            </Stack>
        </Box>
    )
}