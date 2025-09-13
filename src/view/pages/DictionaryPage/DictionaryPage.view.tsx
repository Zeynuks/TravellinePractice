import {
    Button,
    IconButton,
    Menu,
    MenuItem,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    Typography
} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';

import type {useDictionaryPage} from "./DictionaryPage.state.ts";
import {useNavigate} from "react-router-dom";
import {BackButton} from "../../components/BackButton/BackButton.tsx";
import {useState} from "react";

export const DictionaryPageView = ({
                                       dictionary,
                                       deleteWord
                                   }: ReturnType<typeof useDictionaryPage>) => {
    const navigate = useNavigate();
    const [menu, setMenu] = useState<{ id: number; anchor: HTMLElement; } | null>(null);

    return (
        <>
            <Stack direction={"row"} sx={{mb: 3}}>
                <BackButton/>
                <Typography variant="h3" sx={{color: "#364963", ml: 2}}>
                    Словарь
                </Typography>
            </Stack>
            <Button
                variant="contained"
                onClick={() => navigate("/new-word")}
                sx={{
                    mb: 3,
                    textTransform: "uppercase"
                }}
            >
                + Добавить слово
            </Button>
            <Table>
                <TableHead sx={{backgroundColor: "#dfe4ec"}}>
                    <TableRow>
                        <TableCell>Слово на русском языке</TableCell>
                        <TableCell>Перевод на английский язык</TableCell>
                        <TableCell align="right">Действие</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {dictionary.map((word, i) => (
                        <TableRow key={i}>
                            <TableCell>{word.word}</TableCell>
                            <TableCell>{word.translation}</TableCell>
                            <TableCell align="right">
                                <IconButton
                                    aria-label="more"
                                    onClick={(event) => setMenu({id: i, anchor: event.currentTarget})}
                                >
                                    <MenuIcon/>
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Menu
                open={menu !== null}
                anchorEl={menu?.anchor}
                onClose={() => setMenu(null)}>
                <MenuItem
                    onClick={() => {
                        navigate(`/edit-word/${menu!.id}`);
                        setMenu(null);
                    }}
                >
                    Редактировать
                </MenuItem>
                <MenuItem
                    onClick={() => {
                        deleteWord(menu!.id);
                        setMenu(null);
                    }}
                >
                    Удалить
                </MenuItem>
            </Menu>
        </>
    );
};